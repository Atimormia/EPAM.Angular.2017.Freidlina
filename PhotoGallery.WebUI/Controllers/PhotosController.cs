using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PhotoGallery.DB.Infrastructure.Repositories.Abstract;
using PhotoGallery.DB.Model;
using PhotoGallery.Infrastructure.Core;
using PhotoGallery.ViewModels;

namespace PhotoGallery.WebUI.Controllers
{
    public class PhotosController : Controller
    {
        readonly IPhotoRepository _photoRepository;
        readonly ILoggingRepository _loggingRepository;
        public PhotosController(IPhotoRepository photoRepository, ILoggingRepository loggingRepository)
        {
            _photoRepository = photoRepository;
            _loggingRepository = loggingRepository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                List<Photo> photos = _photoRepository
                    .AllIncluding(p => p.Album)
                    .OrderBy(p => p.Id)
                    .ToList();
                IEnumerable<PhotoViewModel> photosVm = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(photos);

                return Json(photosVm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
                return Json(new { status = false });
            }
            
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                Photo photoToRemove = this._photoRepository.GetSingle(id);
                this._photoRepository.Delete(photoToRemove);
                this._photoRepository.Commit();
                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
                return Json(new { status = false });
            }
        }
    }
}
