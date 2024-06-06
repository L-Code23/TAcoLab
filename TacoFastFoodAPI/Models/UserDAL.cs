using Microsoft.AspNetCore.DataProtection;
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc;

namespace TacoFastFoodAPI.Models
{
    
    public class UserDAL
    {
        [HttpPost]
        public static bool  ValidateKey([FromBody] User targetUser, string Apikey)
        {
            
            if (Apikey != targetUser.ApiKey)
            {
                return  true;
            }
            else
            {
                return false;
            }


            return result;
        }
       
        
    }
}
