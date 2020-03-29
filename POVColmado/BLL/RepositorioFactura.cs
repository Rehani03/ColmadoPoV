using POVColmado.DAL;
using POVColmado.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POVColmado.BLL
{
    public class RepositorioFactura
    {
        public bool Guardar(Factura entity)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Factura.Add(entity) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public  bool Modificar(Factura entity)
        {
            bool paso = false;      
            Contexto db = new Contexto();

            try
            {   
                //aqui agrego lo nuevo al detalle
                foreach (var item in entity.Detalles)
                { 
                    if (item.DetalleFacturaId == 0)
                    {
                        db.Entry(item).State = EntityState.Added;
                    }
                    else
                        db.Entry(item).State = EntityState.Modified;
                }
                db.Entry(entity).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public Factura Buscar(int ID)
        {
            Factura factura = new Factura();

            Contexto db = new Contexto();

            try
            {
                factura = db.Factura.Find(ID);
                if (factura != null)
                {
                    factura.Detalles.Count();
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }

            return factura;
        }

    }
}
