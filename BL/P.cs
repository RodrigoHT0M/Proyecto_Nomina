using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class P
    {

        private readonly ListGetAllHistorialPermisoDTO _listGetAllHistorialPermisoDTO;

        public P(ListGetAllHistorialPermisoDTO listGetAllHistorialPermisoDTO) {
            _listGetAllHistorialPermisoDTO = listGetAllHistorialPermisoDTO;
        }
        public ML.Resultado HistorialGetAll()
        {
            ML.Resultado resultado = new ML.Resultado();
            try
            {
                List<GetAllHistorialPermisoDTO> obj = _listGetAllHistorialPermisoDTO.GetAll();

                if (obj.Count > 0)
                {
                    resultado.Objects = new List<object>();
                    foreach (var dto in obj)
                    {
                        ML.HistorialPermiso historialPermiso = ML.HistorialPermisoMapper.Map(dto);
                        resultado.Objects.Add(historialPermiso);


                    }

                    resultado.Correct = resultado.Objects.Count > 0 ? true : false;
                }
                else
                {
                    resultado.Correct = false;
                }


            }
            catch (Exception ex)
            {
                resultado.ErrorMessagge = ex.Message;
                resultado.Correct = false;
            }
            return resultado;
        }
    }
}
