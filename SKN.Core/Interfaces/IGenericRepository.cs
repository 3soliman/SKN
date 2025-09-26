using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SKN.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // الحصول على عنصر بواسطة ID
        Task<T> GetByIdAsync(int id);
        
        // الحصول على جميع العناصر
        Task<IReadOnlyList<T>> GetAllAsync();
        
        // الحصول على عناصر بناء على شرط معين
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        // إضافة عنصر جديد
        Task AddAsync(T entity);
        
        // تحديث عنصر موجود
        void Update(T entity);
        
        // حذف عنصر
        void Delete(T entity);
        
        // حفظ التغييرات في قاعدة البيانات
        Task<int> SaveChangesAsync();
    }
}