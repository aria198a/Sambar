using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace test1api.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string[] strings = { "去開會", "吃午餐", "寫程式" };

            var result = from s in strings
                         where s == "寫程式"
                         select s;
            return result.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        private List<string> strings = new List<string>();
        // POST api/values
        [HttpPost]
        public string Post([FromBody] string value)
        {
            strings.Add(value);

            return "value";
        }
        // POST api/account/vertify
        [HttpPost("verify")]
        public IActionResult VerifyAccount(LoginModel loginModel)
        {
            // 假設這裡有一個用於帳號驗證的方式
            if (VerifyCredentials(loginModel.Username, loginModel.Password))
            {
                // 帳號驗證成功
                return Ok("帳號驗證成功");
            }

            // 帳號驗證失敗
            return Unauthorized("帳號驗證失敗");
        }

        // 模擬帳號驗證的邏輯
        private bool VerifyCredentials(string username, string password)
        {
            // 在這裡實現您的帳號驗證邏輯
            // 比對使用者提供的帳號密碼是否正確
            // 可以使用資料庫查詢、API 呼叫等方式進行帳號驗證

            // 這裡只是一個簡單的範例，假設帳號是 "admin"，密碼是 "password"
            if (username == "admin" && password == "password")
            {
                return true;
            }

            return false;
        }

        // 登入模型
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}

