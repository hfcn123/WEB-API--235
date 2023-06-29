using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApI172.IService;
using WebApI172.Models;
using WebApI172.Service;

namespace WebApI172.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class My172SQLController : ControllerBase
    {
        static readonly IStuManage stuManger = new StuManage();

        #region 查询所有学生信息
        [HttpGet]
        public IEnumerable<Stuinfo> GetAllStuinfo()
        {
            return stuManger.GetAll();
        }
        #endregion

        #region 通过名字获取ID
        [HttpPost("post/GetInfoByName")]
        public Stuinfo GetIdByName(string name)
        {
            var StuItem = stuManger.GetInfo(name);

            if (StuItem != null) { return StuItem.FirstOrDefault(); }
            else
            {
                throw new ArgumentNullException(nameof(name), $"学生信息不存在：{name}");
            }

        }
        #endregion

        #region 根据id获取信息
        [HttpPost("post/Getinfo")]
        public Stuinfo GetinfoById(int id)
        {
            var StuItem = stuManger.GetInfoById(id);

            if (StuItem != null) { return StuItem.FirstOrDefault(); }
            else
            {
                throw new ArgumentNullException(nameof(id), $"学生信息不存在：{id}");
            }

        }
        #endregion

        #region 添加新学生信息
        [HttpPost("post/CreateStuInfo")]
        public IActionResult CreateStu([FromBody] Stuinfo stuDto)
        {
            var createdUser = stuManger.CreateStu(stuDto);

            if (createdUser != null)
            {
                // 添加成功，返回201 Created 状态码和创建的学生信息

                var allStu = stuManger.GetAll();
                return Ok(new { Results="添加成功", AllStu = allStu });

            }
            else
            {
                // 添加失败，返回400 Bad Request 状态码
                return BadRequest("Failed to create student");
            }
        }
        #endregion

        #region 删除

        [HttpDelete("api/deleteStu")]
        public IActionResult DeleteInfo(string stuname)
        {
            if (stuManger.DeleteInfo(stuname))
            {
                var allStu = stuManger.GetAll();
                return Ok(new { Results = "删除成功！", AllStu = allStu });

            }
            else { return BadRequest("删除失败！找不到此学生或此学生已删除！"); }
        }
        #endregion

        #region 修改更新
        [HttpPut("api/UpdataStu")]
        public IActionResult UpdataInfo(string stuname, Stuinfo newstu)
        {
            if (stuManger.UpdataStuInfo(stuname, newstu))
            {
                var allStu = stuManger.GetAll();
                return Ok(new { Results = "修改成功！", AllStu = allStu });
            }
            else { return BadRequest("修改失败！！"); }
        }

        #endregion
   
    }
}
