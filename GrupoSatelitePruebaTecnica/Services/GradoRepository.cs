using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrupoSatelitePruebaTecnica.DataContext;
using GrupoSatelitePruebaTecnica.Models;
using System.Data.Entity;
using System.Web.Mvc;

namespace GrupoSatelitePruebaTecnica.Services
{
    public class GradoRepository
    {
        readonly RegistroEntities db = new RegistroEntities();

        public List<SelectListItem> GetGrados()
        {
            List<SelectListItem> Lst = new List<SelectListItem>();
            try
            {
                var lstG = db.Grado.ToList();
                foreach (var item in lstG)
                {
                    Lst.Add(new SelectListItem
                    {
                        Text = item.Nombre,
                        Value = item.IdGrado.ToString()
                    });
                }
                return Lst;
            }
            catch (Exception e)
            {
                return Lst;
            }
        }
    }
}