using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Context
{

    [MetadataType(typeof(UserMasterData))]
    public  partial class User
    {
        
    }
    [MetadataType(typeof(UserMasterData))]

    public partial class ProductMasterData
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}