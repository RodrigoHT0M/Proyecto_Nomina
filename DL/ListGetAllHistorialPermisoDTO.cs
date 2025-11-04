using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
   
    public class ListGetAllHistorialPermisoDTO
    {
        private readonly SistemaNominaContext _context;

        public ListGetAllHistorialPermisoDTO (SistemaNominaContext context)
        {
            _context = context;
        }       
        public  List<GetAllHistorialPermisoDTO> GetAll()
        {
            return _context.GetAllHistorialPermisoDTO.FromSqlInterpolated($"HistorialPermisoGetAll").ToList();
        }
    }
}
