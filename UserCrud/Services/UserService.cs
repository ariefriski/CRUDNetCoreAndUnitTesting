using Microsoft.EntityFrameworkCore;
using UserCrud.Data;
using UserCrud.Models;
using System.Threading.Tasks;
namespace UserCrud
{

    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        User GetById(int id);
        void Delete(int id);
        void Update(User item);
        Task SaveAsync(User newUser);
    }
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.tbl_user.ToListAsync();
        }

        public async  Task SaveAsync(User newUser)
        {
            _context.tbl_user.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public User GetById(int id)
        {
            return  _context.tbl_user.FirstOrDefault(x=>x.UserId == id);
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                _context.tbl_user.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Update(User item)
        {
            var data =  _context.tbl_user.FirstOrDefault(x => x.UserId == item.UserId);
            if(data != null)
            {
                data.NamaLengkap = item.NamaLengkap;
                data.Username = item.Username;
                data.Password = item.Password;
                data.Status = item.Status;
            }
            _context.tbl_user.Update(data);
            _context.SaveChanges();
        }
    }
}
