using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;
using Result = System.Data.Entity.Core.Objects;
using System.Web;
using Help;
using System.Text;
using System.IO;
using System.Drawing;
using Email;
namespace CMS.Controllers
{
    public class CandidateDetailController : ApiController
    {
        private SANGEntities db = new SANGEntities();
         
        // GET: api/CandidateDetail
        public IQueryable<kmit_CandidateDetail> Getkmit_CandidateDetail()
        {
            return db.kmit_CandidateDetail;
            

        }

        [Route("api/CandidateDetail/getValidAdmin")]
        public List<sp_GetValidAdmin2_Result>  getValidAdmin()
        {
            return db.Database.SqlQuery<sp_GetValidAdmin2_Result>("SElect Username,EmailId from kmit_Admin where IsActive='Y'").ToList();
 
        }
        [Route("api/CandidateDetail/GetAllDepartments")]
        public Result.ObjectResult<string> GetAllDepartments()
        {
            return db.sp_GetAllDepartments();
        }

        // GET: api/CandidateDetail/5
        [ResponseType(typeof(kmit_CandidateDetail))]
        public IHttpActionResult Getkmit_CandidateDetail(string id)
        {
            kmit_CandidateDetail kmit_CandidateDetail = db.kmit_CandidateDetail.Find(id);
            if (kmit_CandidateDetail == null)
            {
                return NotFound();
            }

            return Ok(kmit_CandidateDetail);
        }
        [HttpPost]
        [Route("api/CandidateDetail/SearchCandidateDetail")]
        public Result.ObjectResult<sp_SerarchCandidateDetails_Result> SearchCandidateDetail(SearchFiels searchField)
        {
             
            return db.sp_SerarchCandidateDetails(searchField.SearchCandidateName.Trim(), searchField.SearchCandidatePhoneNo.Trim(), searchField.SearchCandidateDepartment.Trim(), searchField.SearchCandidateStatus.Trim(),searchField.SearchCandidateInterviewer.Trim(),searchField.SearchCandidateReferedBy.Trim(),searchField.SearchCandidateHometown.Trim(),searchField.SearchCandidateCurrentCTC,searchField.SearchCandidateExpectedCTC,searchField.SearchCandidateDateofInterview.Trim(),searchField.SearchCandidateDateOfPhoneIntervbiew.Trim(),searchField.SearchCandidateNoticePeriod,searchField.SearchCandidateExperience,searchField.SearchCandidateEmailId.Trim());
        }

        [HttpPost]
        [Route("api/CandidateDetail/AddCandidateDetail")]
        public Result.ObjectResult<string> AddCandidateDetail(kmit_CandidateDetail candidate)
        {
            return db.sp_addcandidates(candidate.Name, candidate.EmailId, candidate.PhoneNo, candidate.DateofInterview, candidate.DOB, candidate.CurrentCTC, candidate.ExpectedCTC, candidate.Experience, candidate.NoticePeriod, candidate.Interviewer, candidate.CommentsfromInterviewer, candidate.Hometown, candidate.UpdateId, candidate.LastName, candidate.CommentsfromHR, candidate.Status, candidate.Department, candidate.DateOfPhoneIntervbiew, candidate.ReferedBy);
        }

        [HttpPost]
        [Route("api/CandidateDetail/UpdateCandidateDetail")]
        public int UpdateCandidateDetail(kmit_CandidateDetail candidate)
        {
            int result = 0;
            var res = db.sp_UpdateCandidate(candidate.Name, candidate.EmailId, candidate.PhoneNo, candidate.DateofInterview, candidate.DOB, candidate.CurrentCTC, candidate.ExpectedCTC, candidate.Experience, candidate.NoticePeriod, candidate.Interviewer, candidate.CommentsfromInterviewer, candidate.Hometown, candidate.UpdateId, candidate.LastName, candidate.CommentsfromHR, candidate.Status, candidate.Department, candidate.DateOfPhoneIntervbiew,candidate.ReferedBy);
            if (res != null)
            {
                result = 1;
            }

            return result;
        }
        [HttpGet]
        [Route("api/CandidateDetail/DeleteCandidateDetail")]
        public int DeleteCandidateDetail(string Email)
        {
            int result = 0;

            kmit_CandidateDetail Candidate = (from rows in db.kmit_CandidateDetail where rows.EmailId == Email select rows).FirstOrDefault();
            if (Candidate != null)
            {
                var y = db.kmit_CandidateDetail.Remove(Candidate);
                db.SaveChanges();
                result = 1;
            }

            return result;
        }
        [HttpGet]
        [Route("api/CandidateDetail/ValidateCandidateDetail")]
        public Result.ObjectResult<string> ValidateCandidateDetail(string name, string email)
        {

            return db.sp_ValidateUser(name, email);

        }



      [HttpPost]
         [Route("api/CandidateDetail/POstHelp")]
        public HttpResponseMessage POstHelp()
        {
           List<byte[]> byr=new List<byte[]>();
           var req = HttpContext.Current.Request;
          
           string result;
           List<string> fileNameList=new List<string>();
           var fileName = "";
           for (var i = 0; i < req.Files.Count;i++)
           {
               fileName = req.Files.AllKeys[i];
               var file = req.Files[fileName];
               fileName = file.FileName;
               fileNameList.Add(fileName);
               file.SaveAs(HttpContext.Current.Server.MapPath("~/EmailImages/" + fileName));
               byr.Add(File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailImages/" + fileName)));
           }
            //if (file != null)
            //{  
            //    fileName = file.FileName;
            //    file.SaveAs(HttpContext.Current.Server.MapPath("~/EmailImages/" + fileName));
            //    byr = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailImages/" + fileName));
            //}
          
            
            var SuggestionMsg = req["SuggesstionMsg"];
           
            //fileName = new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(" ", "-");
            //fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
           
              SendEmail email=new SendEmail();
              result = email.ThreadMail(byr,fileNameList, "New Suggesstion Request", SuggestionMsg);
             if (result=="Success")
             {
                 return Request.CreateResponse(HttpStatusCode.Created);
             }
             else
             {
                 return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
             }
          
         
        }


      [HttpPost]
      [Route("api/CandidateDetail/POstHelp1")]
      public string POstHelp1(IFile file)
      {

          


          var msg1 = file.FileName;
          var fil1 = file.Base64String; 

          return "success";

      }

      public static byte[] converterDemo(Image x)
      {
          ImageConverter _imageConverter = new ImageConverter();
          byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
          return xByte;
      }
      
    }
}