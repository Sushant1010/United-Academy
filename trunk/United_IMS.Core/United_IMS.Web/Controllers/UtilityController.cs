using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using United_IMS.Web.Security;
using United_IMS.Web.Repository;

namespace United_IMS.Web.Controllers
{
    [CustomAuthorize]
    public class UtilityController : Controller
    {
        GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        // GET: Utlity
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetUnitByItem(int id)
        {
            JsonResult result = new JsonResult();
            var matching = ddl.GetUnitByItemList(id);


            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }


            result.Data = msg;

            return result;

        }

        public JsonResult GetRateByItemUnit(int itemid, int unitid)
        {
            JsonResult result = new JsonResult();
            var matching = ddl.GetRateByItemUnit(itemid, unitid);
            result.Data = matching;
            return result;

        }
        public JsonResult GetStudentByBatchClassSection(int batchid = 0, int classid = 0, int sectionid = 0)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetStudentList(orgid, batchid, classid, sectionid);


            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }

            result.Data = msg;

            return result;
        }
        public JsonResult GetItemByCategory(int catid = 0)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetItemList(orgid, catid);


            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }

            result.Data = msg;

            return result;
        }

        public JsonResult GetItemByCategory2(int catid)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetAssetItemList1(catid);


            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }

            result.Data = msg;

            return result;
        }

        public JsonResult GetItemByCategory1(int catid)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetItemList1(orgid, catid);


            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }

            result.Data = msg;

            return result;
        }

        public JsonResult GetItemByCategoryTransfer(int catid)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetItemListTransfer(orgid, catid);


            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }

            result.Data = msg;

            return result;
        }

        public JsonResult GetOBLbyItem(int asseid)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetOBL(asseid);
            var matching2 = ddl.GetOBLaTrans(asseid);

            string msg = "<option value=''></option>";// "<option>-- Select --</option>";

            foreach (var item in matching)
            {
                if (item.IsTransfer == 0)
                {
                    foreach (var item1 in matching)
                    {
                        msg += "<option value='' selected>" + item1.Name + "</option>";
                    }
                }
                else
                {
                    foreach (var item1 in matching2)
                    {
                        msg += "<option value='' selected>" + item1.Name + "</option>";
                    }
                }

            }
            result.Data = msg;
            return result;
        }

        public JsonResult GetOBLbyItem1(int asseid)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetOBL(asseid);
            var matching2 = ddl.GetOBLaTrans(asseid);

            string msg = "<option value=''></option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                if (item.IsTransfer == 0)
                {
                    foreach (var item1 in matching)
                    {
                        msg += "<option value='' selected>" + item1.Bname + "</option>";
                    }
                }
                else
                {
                    foreach (var item1 in matching2)
                    {
                        msg += "<option value='' selected>" + item1.Bname + "</option>";
                    }
                }
                //msg += "<option value='' selected>" + item.Bname + "</option>";
            }
            result.Data = msg;
            return result;
        }

        public JsonResult GetOBLbyItem2(int asseid)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetOBL(asseid);
            var matching2 = ddl.GetOBLaTrans(asseid);

            string msg = "<option value=''></option>";// "<option>-- Select --</option>";

            foreach (var item in matching)
            {
                if (item.IsTransfer == 0)
                {
                    foreach (var item1 in matching)
                    {
                        msg += "<option value='' selected>" + item1.Lname + "</option>";
                    }
                }
                else
                {
                    foreach (var item1 in matching2)
                    {
                        msg += "<option value='' selected>" + item1.Lname + "</option>";
                    }
                }
                //msg += "<option value='' selected>" + item.Bname + "</option>";
            }
            //foreach (var item in matching)
            //{
            //    msg += "<option value='' selected>" + item.Lname + "</option>";
            //}
            result.Data = msg;
            return result;
        }

        public JsonResult GetBranchByOrgID(int orgaId)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetBranchByOrgId(orgaId);

            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Bname + "</option>";
            }
            result.Data = msg;
            return result;
        }

        public JsonResult GetLocationByOrgID(int orgaId)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetLocationByOrgId(orgaId);

            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Lname + "</option>";
            }
            result.Data = msg;
            return result;
        }

        public JsonResult GetUserByOrgID(int orgaId)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetUserByOrgId(orgaId);

            string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }
            result.Data = msg;
            return result;
        }

        public JsonResult GetLocationName(int locId)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetLocationName(locId);

            string msg = null;// "<option>-- Select --</option>";
            foreach (var item in matching)
            {
                msg += item.Name;
            }

            result.Data = msg;
            return result;
        }

        //public JsonResult GetAssetCode(int assId)
        //{
        //    var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
        //    int orgid = ses.OrganizationId;
        //    JsonResult result = new JsonResult();
        //    var matching = ddl.GetAssetCode(assId);

        //    string msg = null;// "<option>-- Select --</option>";
        //    foreach (var item in matching)
        //    {
        //        msg += item.Name + "-";
        //    }

        //    result.Data = msg;
        //    return result;
        //}

        public JsonResult GetAssetUniqueID(int assId, int LocId)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var assetcode = ddl.GetAssetCode(assId);
            var matching = ddl.GetAssetUniqueID(assId, LocId, orgid);

            string msg = assetcode.ToString() + "-" + matching.ToString();// "<option>-- Select --</option>";

            //foreach (var item in matching)
            //{
            //    msg += item;
            //}

            result.Data = msg;
            return result;
        }

        public JsonResult GetAssetName(string AName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetAssetName(orgid, AName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }

        public JsonResult GetAssetCode(string ACode)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetAssetCode(orgid, ACode);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }
            return result;
        }
        public JsonResult GetStudentRegNo(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetStudentRegNo(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetUnitName(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetUnitName(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetCategoryName(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetCategoryName(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetCategoryCode(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetCategoryCode(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetVendorNames(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetVendorNames(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetItemName(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetItemName(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetBranchName(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetBranchName(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetBranchCode(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetBranchCode(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetLocationNames(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetLocationNames(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetLocationCode(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetLocationCode(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetCategoryNames(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetCategoryNames(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }
        public JsonResult GetCategoryCodes(string ReName)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetCategoryCodes(orgid, ReName);
            if (matching != null)
            {
                result.Data = matching.ToString().ToUpper();
            }

            return result;
        }

        public JsonResult GetStudentTotalByClass(int classid = 0, int sectionid = 0)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            if (sectionid == 0)
            {
                var matching = ddl.GetStudentTotal(orgid, classid);

                result.Data = matching.ToString();
            }
            else
            {
                var matching = ddl.GetStudentTotal(orgid, classid, sectionid);
                result.Data = matching.ToString();
            }
            return result;
        }

        public JsonResult GetStudentByClassSection(int classid = 0, int sectionid = 0)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();

            if (sectionid == 0)
            {
                var matching = ddl.GetClassStudentList(orgid, classid);
                result.Data = matching.ToString();
                string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
                foreach (var item in matching)
                {
                    msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
                }
                result.Data = msg;
                // return result;
            }
            else
            {
                var matching = ddl.GetClassStudentList(orgid, classid, sectionid);
                result.Data = matching.ToString();
                string msg = "<option value=''>-- Select --</option>";// "<option>-- Select --</option>";
                foreach (var item in matching)
                {
                    msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
                }
                result.Data = msg;
                // return result;
            }
            return result;
        }

        public JsonResult GetStudentTotalByClass1(int classid = 0, int sectionid = 0, string studentid = "")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            string StudentId = studentid;
            string[] stuId = StudentId.Split(',');
            int i = 0;
            if (sectionid == 0)
            {

                foreach (var item in stuId)
                {
                    var matching = ddl.GetStudentTotal1(orgid, classid, Convert.ToInt32(stuId[i]));
                    i++;
                }
                result.Data = i.ToString();
            }
            else
            {
                foreach (var item in stuId)
                {
                    var matching = ddl.GetStudentTotal1(orgid, classid, sectionid, Convert.ToInt32(stuId[i]));
                    i++;
                }

                result.Data = i.ToString();
            }
            return result;
        }
        public JsonResult GetDepreciationMethodId(int dep = 0)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var matching = ddl.GetDepreciationid(orgid, dep);




            result.Data = matching;

            return result;


        }
    }
}