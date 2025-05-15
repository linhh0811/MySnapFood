using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.SQL
{
    //Bảng chứa trung gian liên kết
    public abstract class IntermediaryEntity
    {
        /// <summary>
        /// ID cho bản ghi
        /// </summary>
        [Key]
        public Guid Id { get; protected init; }
        /// <summary>
        /// Thời gian tạo cho bản ghi 
        /// </summary>
        private DateTime _created;
        public DateTime Created
        {
            get
            {
                return _created;
            }
            protected set
            {
                _created = value;
            }
        }
        public void FillDataForInsert(Guid IdUser)
        {
            if (IdUser == Guid.Empty)
                throw new ArgumentException("Người dùng không thích hợp để thêm mới.");

            this.Created = DateTime.Now;
        }
    }

    //Bảng chính
    public abstract class BaseDomainEntity
    {
        /// <summary>
        /// ID cho bản ghi
        /// </summary>
        public Guid Id { get; protected init; }

        /// <summary>
        /// Thời gian tạo cho bản ghi 
        /// </summary>
        private DateTime _created;
        public DateTime Created
        {
            get
            {
                return _created;
            }
            protected set
            {
                _created = value;
            }
        }

        /// <summary>
        /// Thời gian chỉnh sửa cuối cho bản ghi (lưu dưới dạng UTC)
        /// </summary>
        private DateTime _lastModified;
        public DateTime LastModified
        {
            get
            {
                return _lastModified;
            }
            protected set
            {
                _lastModified = value;
            }
        }

        /// <summary>
        /// Người tạo đầu tiên
        /// </summary>
        public Guid CreatedBy { get; protected set; }

        /// <summary>
        /// Người sửa cuối cùng
        /// </summary>
        public Guid LastModifiedBy { get; protected set; }

        /// <summary>
        /// Trạng thái duyệt của bản ghi
        /// </summary>
        public ModerationStatus ModerationStatus { get; set; }


        public BaseDomainEntity()
        {
            CreatedBy = default;
            LastModifiedBy = default;
        }

        public void FillDataForInsert(Guid IdUser)
        {
            if (IdUser == Guid.Empty)
                throw new ArgumentException("Người dùng không thích hợp để thêm mới.");            
            this.Created = DateTime.Now;
            this.LastModified = DateTime.Now;
            this.CreatedBy = this.LastModifiedBy = IdUser;
        }

        public void FillDataForUpdate(Guid IdUser)
        {
            if (IdUser == Guid.Empty)
                throw new ArgumentException("Người dùng không thích hợp để cập nhật bản ghi.");

            this.LastModified = DateTime.Now;
            this.LastModifiedBy = IdUser;
        }

    }
}
