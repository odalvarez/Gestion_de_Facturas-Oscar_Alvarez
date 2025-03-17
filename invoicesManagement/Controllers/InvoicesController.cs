using Microsoft.AspNetCore.Mvc;
using invoicesManagement.Data;
using invoicesManagement.Models;

namespace invoicesManagement.Controllers
{
    public class InvoicesController : Controller
    {
        InvoiceData _invoiceData = new InvoiceData();

        public IActionResult List()
        {
            var invoicesList = _invoiceData.List();

            foreach (var item in invoicesList)
            {
                TimeSpan checkExpiration = item.invoiceExpiration - DateTime.Today;
                if (checkExpiration.TotalDays > 3)
                {
                    item.status = 0;
                }
                else if (checkExpiration.TotalDays >= 0)
                {
                    item.status = 1;
                }
                else if (checkExpiration.TotalDays < 0)
                {
                    item.status = 2;
                }
            }
            return View(invoicesList);
        }

        //VISTA
        public IActionResult Insert()
        {
            ViewBag.InsertView = true;
            return View();
        }

        //METODO PARA INSERTAR
        [HttpPost]
        public IActionResult Insert(InvoiceModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.errorValidation = true;
                return View();
            }

            var respuesta = _invoiceData.insertInvoice(model);

            if (respuesta == InvoiceData.statusTransaction.Error)
            {
                ViewBag.error = true;
                return View();
            }

            if (respuesta == InvoiceData.statusTransaction.Duplicated)
            {
                ViewBag.duplicated = true;
                return View();
            }

            TempData["success"] = true;
            return RedirectToAction("List");
        }

        public IActionResult Edit(int id)
        {
            var idInvoice = _invoiceData.List(id)[0];
            return View(idInvoice);
        }

        //METODO PARA EDITAR
        [HttpPost]
        public IActionResult Edit(InvoiceModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.errorValidation = true;
                return View();
            }

            var respuesta = _invoiceData.editInvoice(model);

            if (respuesta == InvoiceData.statusTransaction.Error)
            {
                ViewBag.error = true;
                return View();
            }

            if (respuesta == InvoiceData.statusTransaction.Duplicated)
            {
                ViewBag.duplicated = true;
                return View();
            }

            TempData["success"] = true;
            return RedirectToAction("List");
        }

        //METODO PARA ELIMINAR
        [HttpPost]
        public bool Delete(int id)
        {
            var respuesta = _invoiceData.updateDeletedInvoices(id);

            return respuesta;
        }
    }
}
