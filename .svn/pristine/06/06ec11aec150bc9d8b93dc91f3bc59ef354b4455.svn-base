using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using United_IMS.Web.Repository;
using United_IMS.Web.Models;
using United_IMS.Web.Security;

namespace United_IMS.Web.Controllers
{
    [CustomAuthorize]
    public class ItemController : Controller
    {
		ItemRepo db = new ItemRepo();
		ItemUnitRepo iu = new ItemUnitRepo();
		GetDropdown ddl = new GetDropdown();
		UnitRepo udb = new UnitRepo();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: Item
        public ActionResult Index(string ItemName="",string ItemCode="", int CategoryName = 0)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
          
            ViewBag.CategoryId = new SelectList(ddl.GetCategoryList(orgid), "Id", "Name");
            //ViewBag.ItemName = new SelectList(ddl.GetItemList(orgid), "Id", "Name", ItemName);
            //ViewBag.ItemCode = new SelectList(ddl.GetCategoryList(orgid), "Id", "Name", Category);
            return View(db.GetItemList(orgid,ItemName,ItemCode,CategoryName));
        }


  

        public ActionResult Details(int? id)
		{
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id ==null || id==0)
		{
				return HttpNotFound();
		}

			var item = db.GetItemDetails((int)id);
            if(item.Item==null)
            {
                return HttpNotFound();
            }
			ViewBag.CategoryId = new SelectList(ddl.GetCategoryList(orgid), "Id", "Name");
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(),"Id","Name");
            return View(item);
		}

        public ActionResult Create()
		{
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ViewBag.UnitList = udb.GetUnitList(orgid,"","");
			ViewBag.CategoryId = new SelectList(ddl.GetCategoryList(orgid), "Id", "Name");

			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(FormCollection frm, string[] hddrowpindex)
		{
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            try
			{
				MS_Item item = new MS_Item();
				item.ItemName = frm["ItemName"];
				//item.ItemCode = frm["ItemCode"];
				//item.ItemDescription = frm["ItemDescription"];
                item.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
				item.CategoryId = Convert.ToInt32(frm["CategoryId"]);
				item.EnteredBy = (User as CustomPrincipal).UserId;
				item.EnteredDate = DateTime.Now;
				int itemid = db.InsertItem(item);
				MS_ItemUnit uitem;
                if(hddrowpindex!=null && hddrowpindex.Count()>0)
				foreach(var unititem in hddrowpindex)
				{
					uitem = new MS_ItemUnit();
					uitem.UnitId = Convert.ToInt32(frm["UnitId-"+unititem]);
					uitem.UnitSellingPrice = Convert.ToDecimal(frm["SellingPrice-" + unititem]);
					uitem.QuantityInPiece= Convert.ToInt32(frm["QuantityPer-" + unititem]);
					if (frm["IsDefault-" + unititem] != null)
					{ uitem.IsDefault = true; }
					else 
					{ uitem.IsDefault = false; }
					uitem.OrganizationId = item.OrganizationId;
					uitem.ItemId = itemid;
					iu.InsertItemUnit(uitem);
				}
			}catch (Exception ex)
			{
				return View();
			 }
			ViewBag.UnitList = udb.GetUnitList(orgid, "", "");
			ViewBag.CategoryId = new SelectList(ddl.GetCategoryList(orgid), "Id", "Name");
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
			return RedirectToAction("Index");
		}

        public ActionResult Edit(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            var item = db.GetItemDetails((int)id);
            if(item.Item==null)
            {
                return HttpNotFound();
            }
            ViewBag.UnitList = udb.GetUnitList(orgid, "", "");

            ViewBag.CategoryId = new SelectList(ddl.GetCategoryList(orgid), "Id", "Name",item.Item.CategoryId);
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", item.Item.OrganizationId);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,FormCollection frm, string[] hddrowpindex)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            try
            {
                MS_Item item = db.GetItemById(id);// (Convert.ToInt32(frm["ItemId"]));
                item.ItemName = frm["ItemName"];
                //item.ItemCode = frm["ItemCode"];
                //item.ItemDescription = frm["ItemDescription"];
                item.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
                item.CategoryId = Convert.ToInt32(frm["CategoryId"]);
                item.LastUpdatedBy = (User as CustomPrincipal).UserId;
                item.LastUpdatedDate = DateTime.Now;
                db.UpdateItem(item);
                iu.DeleteAllItemUnitByItem(item.ItemId,0, DateTime.Now);
                MS_ItemUnit uitem;
                if (hddrowpindex != null && hddrowpindex.Count() > 0)
                    foreach (var unititem in hddrowpindex)
                {
                    if (frm["ItemUnitId-" + unititem] != "0")
                        uitem = iu.GetItemUnitById(Convert.ToInt32(frm["ItemUnitId-" + unititem]));
                    else
                        uitem = new MS_ItemUnit();
                    uitem.UnitId = Convert.ToInt32(frm["UnitId-" + unititem]);
                    uitem.UnitSellingPrice = Convert.ToDecimal(frm["SellingPrice-" + unititem]);
                    uitem.QuantityInPiece = Convert.ToInt32(frm["QuantityPer-" + unititem]);
                    if (frm["IsDefault-" + unititem] != null)
                    { uitem.IsDefault = true; }
                    else
                    { uitem.IsDefault = false; }
                    uitem.OrganizationId = item.OrganizationId;
                    uitem.ItemId = item.ItemId;
                    if (frm["ItemUnitId-" + unititem] != "0")
                        iu.UpdateItemUnit(uitem);
                    else
                        iu.InsertItemUnit(uitem);
                }
            }
            catch (Exception ex)
            {
                //ViewBag.UnitList = udb.GetUnitList(0, "", "");
                //ViewBag.CategoryId = new SelectList(ddl.GetCategoryList(), "Id", "Name", frm["CategoryId"]);
                //ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", frm["OrganizationId"]);

                return RedirectToAction("Edit",new { id=frm["ItemId"]});
            }
            //ViewBag.UnitList = udb.GetUnitList(0, "", "");
            //ViewBag.CategoryId = new SelectList(ddl.GetCategoryList(), "Id", "Name", frm["CategoryId"]);
            //ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", frm["OrganizationId"]);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            db.DeleteItem((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            iu.DeleteAllItemUnitByItem((int)id,0,DateTime.Now);
            return RedirectToAction("Index");
        }
    }
}
