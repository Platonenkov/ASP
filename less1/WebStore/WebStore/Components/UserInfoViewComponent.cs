using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Components
{
    [ViewComponent(Name ="UserInfo")] //имя компоненту дать тут
    public class UserInfoViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            if (User.Identity.IsAuthenticated)
                return View("UserInfoView");
            return View();
        }
    }
}
