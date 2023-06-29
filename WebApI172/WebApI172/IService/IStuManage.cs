using WebApI172.Models;

namespace WebApI172.IService
{
    public interface IStuManage
    {
        //获取所有学生信息
        IEnumerable<Stuinfo> GetAll();
        //根据名字获取ID
        List<Stuinfo> GetInfo(string name);
        List<Stuinfo> GetInfoById(int id);
        //添加
        Stuinfo CreateStu(Stuinfo stuDto);
        //删除
        Boolean DeleteInfo(string stuname);
        Boolean UpdataStuInfo(string stuname,Stuinfo newstu);
    }

}
