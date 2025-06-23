using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PromotionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        #region get dữ liệu     
        public List<PromotionDto> GetPromotionActivate()
        {
            var promotions = _unitOfWork.PromotionRepository.FindWhere(x=>x.StartDate<=DateTime.Now&&x.EndDate>DateTime.Now&&x.ModerationStatus==ModerationStatus.Approved).ToList()
                .Select(p => new PromotionDto
                {
                    Id = p.Id,
                    PromotionName = p.PromotionName,
                    PromotionType = p.PromotionType,
                    PromotionValue = p.PromotionValue,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    ModerationStatus = p.ModerationStatus,
                    Created = p.Created,
                    LastModified = p.LastModified,
                    CreatedBy = p.CreatedBy,
                    LastModifiedBy = p.LastModifiedBy,
                    Description=p.Description,
                   
                    PromotionItems = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.PromotionId == p.Id).Select(x => new PromotionItemDto()
                    {
                        Id = x.Id,
                        PromotionId = x.PromotionId,
                        ItemId = x.ItemId,
                        ItemType = x.ItemType
                    }).ToList()
                }).ToList();
            foreach (var item in promotions)
            {
                foreach (var i in item.PromotionItems)
                {
                    if (i.ItemType==ItemType.Product)
                    {
                        var product = _unitOfWork.ProductRepo.GetById(i.ItemId);
                        if (product is not null)
                        {
                            i.ItemName = product.ProductName;
                            i.ImageUrl = product.ImageUrl;
                            i.BasePrice=product.BasePrice;
                            i.SizeName = GetSizeNameById(product.SizeId??Guid.Empty);
                            i.CategoryName = GetCategoryNameById(product.CategoryId);

                        }

                    }
                    else if (i.ItemType==ItemType.Combo)
                    {
                        var combo = _unitOfWork.ComboRepo.GetById(i.ItemId);

                        if (combo is not null)
                        {
                            i.ItemName = combo.ComboName;
                            i.ImageUrl = combo.ImageUrl;
                            i.BasePrice = combo.BasePrice;
                            i.ComboItems = _unitOfWork.ProductComboRepo
                                .FindWhere(x => x.ComboId == i.ItemId)
                                .Select(x => new ComboProductDto
                                {
                                    ProductId = x.ProductId,
                                    Quantity = x.Quantity,
                                    ProductName = x.Quantity + " " + _unitOfWork.ProductRepo.GetById(x.ProductId)?.ProductName,
                       
                                })
                                .ToList();

                        }
                    }
                }
            }
            return promotions;
        }

        public async Task<PromotionDto?> GetByIdAsync(Guid id)
        {
            var promotion =await _unitOfWork.PromotionRepository.GetByIdAsync(id);
            if (promotion == null)
                return null;
            var promotionDto = new PromotionDto()
            {
                Id = promotion.Id,
                PromotionName = promotion.PromotionName,
                PromotionType = promotion.PromotionType,
                PromotionValue = promotion.PromotionValue,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                ModerationStatus = promotion.ModerationStatus,
                Created = promotion.Created,
                LastModified = promotion.LastModified,
                CreatedBy = promotion.CreatedBy,
                LastModifiedBy = promotion.LastModifiedBy,
                Description = promotion.Description,
                PromotionItems = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.PromotionId == promotion.Id).Select(x => new PromotionItemDto()
                {
                    Id = x.Id,
                    PromotionId = x.PromotionId,
                    ItemId = x.ItemId,
                    ItemType = x.ItemType
                }).ToList()
            };
            return promotionDto;
        }

        public DataTableJson GetPaged(BaseQuery query)
        {
            try
            {
                int totalRecords = 0;
                var dataQuery = _unitOfWork.PromotionRepository.FilterData(
                    q => q, // Bỏ hoàn toàn điều kiện Where
                    query.gridRequest,
                    ref totalRecords
                );
                var data = dataQuery.ToList()
                .Select((m, i) => new PromotionDto
                {
                    Id = m.Id,
                    Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                    PromotionName = m.PromotionName,
                    PromotionType = m.PromotionType,
                    PromotionValue = m.PromotionValue,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Created = m.Created,
                    LastModified = m.LastModified,
                    ModerationStatus = m.ModerationStatus,        
                    CreatedBy = m.CreatedBy,
                    LastModifiedBy = m.LastModifiedBy,
                    Description=m.Description,
                    PromotionItems = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.PromotionId == m.Id).Select(x => new PromotionItemDto()
                    {
                        Id = x.Id,
                        PromotionId = x.PromotionId,
                        ItemId = x.ItemId,
                        ItemType = x.ItemType
                    }).ToList()
                });

                foreach (var promotion in data)
                {
                    foreach(var promotionItem in promotion.PromotionItems)
                    {
                        if (promotionItem.ItemType== Domain.Enums.ItemType.Product)
                        {
                            var product = _unitOfWork.ProductRepo.GetById(promotionItem.ItemId);
                            if (product is not null)
                            {
                                promotionItem.ItemName = product.ProductName;
                                promotionItem.ImageUrl = product.ImageUrl;
                                promotionItem.BasePrice = product.BasePrice;
                            }
                        }
                        else if(promotionItem.ItemType == Domain.Enums.ItemType.Combo) { }
                    }
                }

                DataTableJson dataTableJson = new DataTableJson(data, query.draw, totalRecords);
                dataTableJson.querytext = dataQuery.ToString();
                return dataTableJson;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        #endregion

        #region thêm sửa xóa
        public async Task<Guid> CreateAsync(PromotionDto item)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(item.PromotionName))
                {
                    throw new Exception("Tên khuyến mại trống");
                }
            
                if (item.PromotionValue <= 0)
                {
                    throw new Exception("Giá trị nhỏ hơn 0");
                }
                if (item.PromotionItems is null || !item.PromotionItems.Any())
                {
                    throw new Exception("Sản phẩm trống");
                }
                Promotions promotion = new Promotions
                {
                    PromotionName = item.PromotionName,
                    PromotionValue = item.PromotionValue,
                    PromotionType = item.PromotionType,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    ModerationStatus = ModerationStatus.Pending,
                    Description = item.Description,
                };
                _unitOfWork.PromotionRepository.Add(promotion);
                await _unitOfWork.CompleteAsync();
                if (item.PromotionItems != null && item.PromotionItems.Any())
                    {
                    foreach (var promotionItemDto in item.PromotionItems)
                    {
                        if (promotionItemDto.ItemType==Domain.Enums.ItemType.Product)
                        {
                            var promotionItem = new PromotionItem
                            {
                                PromotionId = promotion.Id,
                                ProductId = promotionItemDto.ItemId, 
                                ItemId = promotionItemDto.ItemId,
                                ItemType = promotionItemDto.ItemType,
                            };

                            _unitOfWork.PromotionItemsRepository.Add(promotionItem);
                        }else if(promotionItemDto.ItemType == Domain.Enums.ItemType.Combo)
                        {
                            var promotionItem = new PromotionItem
                            {
                                PromotionId = promotion.Id,
                                ComboId = promotionItemDto.ItemId,
                                ItemId = promotionItemDto.ItemId,
                                ItemType = promotionItemDto.ItemType,
                            };
                            _unitOfWork.PromotionItemsRepository.Add(promotionItem);
                        }
                        else
                        {
                            throw new Exception("Loại sản phẩm không hợp lệ");
                        }

                    }

                    await _unitOfWork.CompleteAsync();
                }
                await _unitOfWork.CommitAsync();
                return promotion.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Lỗi khi tạo khuyến mại: " + ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, PromotionDto item)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var promotion = await _unitOfWork.PromotionRepository.GetByIdAsync(id);
                if (promotion is null)
                {
                    throw new Exception("Không tìm thấy combo");
                }
                if (string.IsNullOrEmpty(item.PromotionName))
                {
                    throw new Exception("Tên khuyến mại trống");
                }

                if (item.PromotionValue <= 0)
                {
                    throw new Exception("Giá trị nhỏ hơn 0");
                }
                if (item.PromotionItems is null || !item.PromotionItems.Any())
                {
                    throw new Exception("Sản phẩm trống");
                }

              
                promotion.PromotionName = item.PromotionName;
                promotion.PromotionType = item.PromotionType;
                promotion.PromotionValue = item.PromotionValue;
                promotion.StartDate = item.StartDate;
                promotion.EndDate = item.EndDate;
                promotion.Description = item.Description;

                _unitOfWork.PromotionRepository.Update(promotion);
                await _unitOfWork.CompleteAsync();

                var promotionItemDelete = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.PromotionId == id);
                _unitOfWork.PromotionItemsRepository.DeleteRange(promotionItemDelete);
                await _unitOfWork.CompleteAsync();

                if (item.PromotionItems != null && item.PromotionItems.Any())
                {
                    foreach (var promotionItemDto in item.PromotionItems)
                    {
                        if (promotionItemDto.ItemType == Domain.Enums.ItemType.Product)
                        {
                            var promotionItem = new PromotionItem
                            {
                                PromotionId = promotion.Id,
                                ProductId = promotionItemDto.ItemId,
                                ItemId = promotionItemDto.ItemId,
                                ItemType = promotionItemDto.ItemType,
                            };

                            _unitOfWork.PromotionItemsRepository.Add(promotionItem);
                        }
                        else if (promotionItemDto.ItemType == Domain.Enums.ItemType.Combo)
                        {
                            var promotionItem = new PromotionItem
                            {
                                PromotionId = promotion.Id,
                                ComboId = promotionItemDto.ItemId,
                                ItemId = promotionItemDto.ItemId,
                                ItemType = promotionItemDto.ItemType,
                            };
                            _unitOfWork.PromotionItemsRepository.Add(promotionItem);
                        }
                        else
                        {
                            throw new Exception("Loại sản phẩm không hợp lệ");
                        }
                    }
                    await _unitOfWork.CompleteAsync();

                }
                await _unitOfWork.CommitAsync();

                return true;

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var promotion =await _unitOfWork.PromotionRepository.GetByIdAsync(id);
            if (promotion == null)
                return false;
            _unitOfWork.PromotionRepository.Delete(promotion);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        #endregion

        #region duyệt, hủy duyệt
        public async Task<bool> ApproveAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var promotion = await _unitOfWork.PromotionRepository.GetByIdAsync(id);
            if (promotion == null)
                return false;

            promotion.ModerationStatus = ModerationStatus.Approved;
            _unitOfWork.PromotionRepository.Update(promotion);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> RejectAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var promotion = await _unitOfWork.PromotionRepository.GetByIdAsync(id);
            if (promotion == null)
                return false;

            promotion.ModerationStatus = ModerationStatus.Rejected;
            _unitOfWork.PromotionRepository.Update(promotion);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        #endregion

        private string GetCategoryNameById(Guid id)
        {

            var category = _unitOfWork.CategoriesRepo.GetById(id);
            if (category is null)
            {
                return string.Empty;
            }
            else
            {
                return category.CategoryName;
            }
        }
        private string? GetSizeNameById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            var sizes = _unitOfWork.SizesRepo.GetById(id);
            if (sizes is not null)
            {
                var sizeChild = _unitOfWork.SizesRepo.FindWhere(x => x.ParentId == sizes.Id && x.ModerationStatus == ModerationStatus.Approved).OrderBy(x => x.DisplayOrder);
                var nameSize = sizes.SizeName + "(" + string.Join(", ", sizeChild.Select(x => x.SizeName)) + ")";
                return nameSize;
            }
            else
            {
                return null;
            }

        }

    }
}
