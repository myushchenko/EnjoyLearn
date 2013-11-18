using EnjoyLearn.Data.Core.Interfaces;

namespace EnjoyLearn.Web.Controllers.API
{
  using EnjoyLearn.Models;
  using NLog.Mvc;
  using System;
  using System.Linq;
  using System.Net;
  using System.Net.Http;

  public class DictionaryController : BaseApiController
  {
    //private readonly IDictionaryRepository _repository;
    private readonly IEntityRepository<DictionaryModel> _repository;

    public DictionaryController(ILogger logger, IEntityRepository<DictionaryModel> repository)
      : base(logger)
    {
      this._repository = repository;
    }

    //GET
    public HttpResponseMessage Get()
    {
      var learnWord = _repository.GetAll();//.Where(s => s. == 1);//.Take(10);

      var delimiters = new char[] { ',', ';', '/' };


      // var dictionaryModels = learnWord as DictionaryModel[] ?? learnWord.ToArray();
      foreach (var dictionaryModel in learnWord)
      {
        if (dictionaryModel.RussianWord != null)
        {
          var words = dictionaryModel.RussianWord.Split(delimiters);
          if (words.Count() > 2)
          {
            dictionaryModel.RussianWord = words[0];
          }
        }
      }
      return Request.CreateResponse(HttpStatusCode.OK, learnWord);

    }

    // POST
    public HttpResponseMessage Post(DictionaryModel word)
    {
      if (ModelState.IsValid)
      {
        // word.UserId = 1;
        word.Id = Guid.NewGuid();
        _repository.Add(word);
        _repository.Save();
        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, word);
        return response;
      }
      else
      {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }
    }

    // PUT
    public HttpResponseMessage Put(DictionaryModel model)
    {
      if (!ModelState.IsValid)
      {
        return Request.CreateErrorResponse(
            HttpStatusCode.BadRequest, ModelState);
      }
      /* var word = _dictionaryService.GetById(model.Id);
       if ((word == null) || (word.UserId != 1))
       {
         return Request.CreateResponse(HttpStatusCode.NotFound);
       }*/
      //_repository.Update(model);
      return Request.CreateResponse(HttpStatusCode.NoContent);
    }

    // DELETE 
    public HttpResponseMessage Delete(Guid id)
    {
      /*var word = _repository.GetById(id);
       if (word == null)
       {
         return Request.CreateResponse(HttpStatusCode.NotFound);
       }

       _repository.Delete(word);
        return Request.CreateResponse(HttpStatusCode.OK, word);
       */
      return Request.CreateResponse(HttpStatusCode.OK);
    }
  }
}
