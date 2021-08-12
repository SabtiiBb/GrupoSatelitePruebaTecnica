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
    public class AlumnoRepository
    {
        readonly RegistroEntities db = new RegistroEntities();

        public List<SelectListItem> GetGrados()
        {
            List<SelectListItem> Lst = new List<SelectListItem>();
            try
            {
                var lstG = db.Grado.ToList();
                foreach(var item in lstG)
                {
                    Lst.Add(new SelectListItem
                    {
                        Text = item.Nombre,
                        Value = item.IdGrado.ToString()
                    });
                }
                return Lst;
            }catch(Exception e)
            {
                return Lst;
            }
        }

        public AlumnoVM GetListadoAlumnos()
        {
            AlumnoVM model = new AlumnoVM();
            model.ListAlumnos = new List<AlumnoVM>();
            try
            {
                var list = db.Alumno
                    .Include("Grado")
                    .ToList();
                foreach(var item in list)
                {
                    model.ListAlumnos.Add(new AlumnoVM
                    {
                        idAlumno = item.IdAlm,
                        codAlumno = item.CodigoAlm,
                        Nombre = item.Nombre,
                        Edad = (int)item.Edad,
                        IdGrado = item.IdGrado,
                        NombreGrado = item.Grado.Nombre,
                        Genero = item.Genero,
                        Observacion = item.Observacion
                    });
                }

                return model;
            }catch(Exception e)
            {
                return model;
            }
        }

        public AlumnoVM getAlumno(int id)
        {
            try
            {
                Alumno DbModel = db.Alumno.Where(x => x.IdAlm == id).Include("Grado").Single();
                AlumnoVM model = new AlumnoVM
                {
                    idAlumno = DbModel.IdAlm,
                    codAlumno = DbModel.CodigoAlm,
                    Nombre = DbModel.Nombre,
                    Edad = (int)DbModel.Edad,
                    Observacion = DbModel.Observacion,
                    Genero = DbModel.Genero,
                    IdGrado = DbModel.IdGrado,
                    NombreGrado = DbModel.Grado.Nombre
                };

                return model;
            }catch(Exception e)
            {
                AlumnoVM model = new AlumnoVM();
                return model;
            }
        }

        public bool crearAlumno(AlumnoVM model)
        {
            bool Exito;
            Alumno DbModel = new Alumno
            {
                CodigoAlm = model.codAlumno,
                Nombre = model.Nombre,
                Edad = model.Edad,
                Genero = model.Genero,
                IdGrado = model.IdGrado,
                Observacion = model.Observacion
            };
            try
            {
                db.Alumno.Add(DbModel);
                db.SaveChanges();
                Exito = true;
            }catch(Exception e)
            {
                Exito = false;
            }
            return Exito;
        }

        public bool EditarAlumno(AlumnoVM model)
        {
            bool Exito;
            try
            {
                Alumno DbModel = db.Alumno.Where(x => x.IdAlm == model.idAlumno).Single();
                DbModel.CodigoAlm = model.codAlumno;
                DbModel.Nombre = model.Nombre;
                DbModel.Edad = model.Edad;
                DbModel.Genero = model.Genero;
                DbModel.IdGrado = model.IdGrado;
                DbModel.Observacion = model.Observacion;

                db.Entry(DbModel).State = EntityState.Modified;
                db.SaveChanges();
                Exito = true;
            }
            catch (Exception e)
            {
                Exito = false;
            }
            return Exito;
        }

        public bool EliminarAlumno(int id)
        {
            bool Exito;
            try
            {
                Alumno model = db.Alumno.Where(x => x.IdAlm == id).Single();
                db.Alumno.Remove(model);
                db.SaveChanges();
                Exito = true;
            }
            catch(Exception e)
            {
                Exito = false;
            }

            return Exito;
        }
    }
}