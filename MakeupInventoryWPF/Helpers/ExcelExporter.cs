using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using MakeupInventoryWPF.Data;

namespace MakeupInventoryWPF.Helpers
{
    public static class ExcelExporter
    {
        public static void ExportMovementsToExcel(DateTime start, DateTime end, string filePath)
        {
            var db = new InventoryDbContext();
            var movimientos = db.ProductMovements
                .Where(m => m.MovementDate >= start && m.MovementDate <= end)
                .OrderByDescending(m => m.MovementDate)
                .ToList();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Movimientos");

            // Encabezados
            worksheet.Cell(1, 1).Value = "Fecha";
            worksheet.Cell(1, 2).Value = "SKU";
            worksheet.Cell(1, 3).Value = "Producto";
            worksheet.Cell(1, 4).Value = "Tipo";
            worksheet.Cell(1, 5).Value = "Antes";
            worksheet.Cell(1, 6).Value = "Después";
            worksheet.Cell(1, 7).Value = "Motivo";
            worksheet.Cell(1, 8).Value = "Empleado";

            // Datos
            int row = 2;
            foreach (var m in movimientos)
            {
                worksheet.Cell(row, 1).Value = m.MovementDate;
                worksheet.Cell(row, 2).Value = m.SKU;
                worksheet.Cell(row, 3).Value = m.ProductName;
                worksheet.Cell(row, 4).Value = m.MovementType;
                worksheet.Cell(row, 5).Value = m.ProductStockBeforeMovement;
                worksheet.Cell(row, 6).Value = m.ProductStockAfterMovement;
                worksheet.Cell(row, 7).Value = m.Reason;
                worksheet.Cell(row, 8).Value = $"{m.EmployeeID} - {m.EmployeeName}";
                row++;
            }

            // Ajuste automático
            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(filePath);
        }
    }
}
