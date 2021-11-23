using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmigoSecreto.Helpers
{
    public class SelectListItemHelper
    {
        public static IEnumerable<SelectListItem> Genero()
        {
            IList<SelectListItem> selectGenero = new List<SelectListItem>
            {
                 new SelectListItem{Text = "Masculino", Value = "M"},
                  new SelectListItem{Text = "Feminino", Value = "F"}

            };
            return selectGenero;
        }
    }
}