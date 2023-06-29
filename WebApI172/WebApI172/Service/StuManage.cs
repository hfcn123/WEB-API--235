using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;
using WebApI172.IService;
using WebApI172.Models;

namespace WebApI172.Service
{
    public class StuManage : IStuManage
    {
        StudentsContext stu = new StudentsContext();
        //添加
        public Stuinfo CreateStu(Stuinfo stuDto)
        {

            var stuinfo = new Stuinfo { Stuid = stuDto.Stuid, Stusex = stuDto.Stusex, Stuname = stuDto.Stuname };
            // 在数据库中添加新用户
            stu.Stuinfos.Add(stuinfo);
            stu.SaveChanges();

            return stuinfo;
        }
        //删除
        public bool DeleteInfo(string stuname)
        {
            var dellist = stu.Stuinfos.FirstOrDefault(x => x.Stuname.Equals(stuname));
            if (dellist!=null) 
            {
              stu.Stuinfos.Remove(dellist);
                stu.SaveChanges() ;
                return true;
            } 
            else 
            {
                return false;
            }
        }

        public IEnumerable<Stuinfo> GetAll()
        {
            return stu.Stuinfos.ToList();
        }
        //查询
        public List<Stuinfo> GetInfo(string name)
        {
            var list = stu.Stuinfos.Where(x => x.Stuname.Equals(name)).ToList();
            if (list == null)
            {
                throw new ArgumentNullException(nameof(name), $"学生信息不存在：{name}");
            }
            else
            {
                return list;
            }

        }

     

        public List<Stuinfo> GetInfoById(int id)
        {
            var list = stu.Stuinfos.Where(x => x.Stuid == id).ToList();
            if (list == null)
            {
                throw new ArgumentNullException(nameof(id), $"学生信息不存在：{id}");
            }
            else
            {
                return list;
            }
        }

        public Boolean UpdataStuInfo(string stuname, Stuinfo newstu)
        {
            var list=stu.Stuinfos.FirstOrDefault(x => x.Stuname.Equals(stuname));
            if (list!=null) 
            { 
                list.Stuid=newstu.Stuid;
                list.Stuname=newstu.Stuname;
                list.Stusex=newstu.Stusex;
                //stu.Stuinfos.Add(list); 
             int i=stu.SaveChanges();
                if(i>0) { return true; }else { return false; }
            }
            else
            { throw new ArgumentNullException(nameof(stuname), $"学生信息不存在：{stuname}"); }
        }
    }
}
