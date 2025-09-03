using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }


        #region Get dữ liệu

        public async Task<List<Sizes>> GetAllAsync()
        {
            var sizes =await _unitOfWork.SizesRepo.GetAllAsync();
            return sizes.ToList();
        }

        public async Task<Sizes?> GetByIdAsync(Guid id)
        {
           var size =await _unitOfWork.SizesRepo.GetByIdAsync(id);
            return size;
        }

        public async Task<List<SizeTreeDto>> GetSizeTreeAsync()
        {
            var sizes =await _unitOfWork.SizesRepo.GetAllAsync();
            var sizeTree = sizes.Where(x=>x.ParentId==null)
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new SizeTreeDto
                {
                    Id = x.Id,
                    SizeName = x.SizeName,
                    DisplayOrder = x.DisplayOrder,
                    ModerationStatus = x.ModerationStatus,
                    Children= GetChildSize(sizes.ToList(), x.Id)

                }).ToList();
            return sizeTree;
        }
        public  List<Sizes> GetSizeSelect()
        {
            var sizes =  _unitOfWork.SizesRepo.FindWhere(x=>x.ParentId==null)
                .OrderBy(x=>x.DisplayOrder);
            foreach (var item in sizes)
            {
                var sizeChild = _unitOfWork.SizesRepo.FindWhere(x => x.ParentId == item.Id);
                item.SizeName = item.SizeName + "("+ string.Join(", ", sizeChild.Select(x=>x.SizeName)) + ")";
                
            }
            return sizes.ToList();
        }
        #endregion
        #region Thêm, sửa, xóa
        public async Task<bool> UpdateAsync(Guid id, SizeDto item)
        {
            try
            {
                var size = await _unitOfWork.SizesRepo.GetByIdAsync(id);
                if (size is null)
                {
                    throw new Exception("Không tìm thấy size");
                }

                if (item is null)
                {
                    throw new Exception("Size trống");
                }

                if (string.IsNullOrEmpty(item.SizeName))
                {
                    throw new Exception("Tên size trống");
                }

                if (item.AdditionalPrice<0)
                {
                    throw new Exception("Giá nhỏ hơn 0");
                }

                if (item.DisplayOrder < 0)
                {
                    throw new Exception("Vị trí nhỏ hơn 0");
                }

                size.SizeName = item.SizeName.Trim();
                size.AdditionalPrice = item.AdditionalPrice;
                size.DisplayOrder = item.DisplayOrder;
                size.ParentId = item.ParentId;
                _unitOfWork.SizesRepo.Update(size);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Guid> CreateAsync(SizeDto item)
        {
            try
            {
                if (item is null)
                {
                    throw new Exception("Size trống");
                }

                if (string.IsNullOrEmpty(item.SizeName))
                {
                    throw new Exception("Tên size trống");
                }

                if (item.AdditionalPrice < 0)
                {
                    throw new Exception("Giá nhỏ hơn 0");
                }

                if (item.DisplayOrder < 0)
                {
                    throw new Exception("Vị trí nhỏ hơn 0");
                }

                Sizes size = new Sizes
                {
                    SizeName = item.SizeName.Trim(),
                    AdditionalPrice = item.AdditionalPrice,
                    DisplayOrder = item.DisplayOrder,
                    ParentId = item.ParentId,
                };
                _unitOfWork.SizesRepo.Add(size);
                await _unitOfWork.CommitAsync();
                return size.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<bool> DeleteAsync(Guid id)
        {
            var size = await _unitOfWork.SizesRepo.GetByIdAsync(id);
            if (size is null)
            {
                throw new Exception("Không tìm thấy size");
            }
            _unitOfWork.SizesRepo.Delete(size);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        #endregion
        #region Duyệt, hủy duyệt
        
        public async Task<bool> ApproveAsync(Guid id)//Duyệt
        {
            var size = _unitOfWork.SizesRepo.GetById(id);
            if (size is not null)
            {
                size.ModerationStatus = ModerationStatus.Approved;
                _unitOfWork.SizesRepo.Update(size);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public int CheckReject(Guid id) //Hủy duyệt
        {
            var ProductApprovedCount = _unitOfWork.ProductRepo
                .FindWhere(x => x.SizeId == id && x.ModerationStatus == ModerationStatus.Approved)
                .Select(x => x.Id)
                .Distinct()
                .Count();

            return ProductApprovedCount;
        }

        public async Task<bool> RejectAsync(Guid id) //Hủy duyệt
        {
            var size = _unitOfWork.SizesRepo.GetById(id);
            if (size is not null)
            {
                size.ModerationStatus = ModerationStatus.Rejected;
                _unitOfWork.SizesRepo.Update(size);
                await _unitOfWork.CompleteAsync();
                var productIds = _unitOfWork.ProductRepo
                    .FindWhere(x => x.SizeId == id && x.ModerationStatus == ModerationStatus.Approved)
                    .Select(x => x.Id);
                foreach (var productId in productIds)
                {
                    var product = _unitOfWork.ProductRepo.GetById(productId);
                    if (product is not null)
                    {
                        product.ModerationStatus = ModerationStatus.Rejected;
                        _unitOfWork.ProductRepo.Update(product);
                        await _unitOfWork.CompleteAsync();
                        var comboIds = _unitOfWork.ProductComboRepo.FindWhere(x => x.ProductId == productId).Select(x => x.ComboId);
                        var ComboApprovedIds = _unitOfWork.ComboRepo
                           .FindWhere(x => comboIds.Contains(x.Id) && x.ModerationStatus == ModerationStatus.Approved)
                           .Select(x => x.Id);
                        if (ComboApprovedIds.Count() > 0)
                        {
                            foreach (var comboId in ComboApprovedIds)
                            {
                                var combo = _unitOfWork.ComboRepo.GetById(comboId);
                                if (combo is not null)
                                {
                                    combo.ModerationStatus = ModerationStatus.Rejected;
                                    _unitOfWork.ComboRepo.Update(combo);
                                    await _unitOfWork.CompleteAsync();
                                }

                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        #endregion
        private List<SizeTreeDto> GetChildSize(List<Sizes> sizes, Guid parrentId)
        {
            var childSize = sizes
               .Where(x => x.ParentId == parrentId)
               .OrderBy(x => x.DisplayOrder)
               .Select(x => new SizeTreeDto
               {
                   Id = x.Id,
                   SizeName = x.SizeName,
                   ParentId = x.ParentId,
                   DisplayOrder = x.DisplayOrder,
                   AdditionalPrice = x.AdditionalPrice,
                   ModerationStatus=x.ModerationStatus
               })
               .ToList();

            return childSize;

        }

     
    }
}
