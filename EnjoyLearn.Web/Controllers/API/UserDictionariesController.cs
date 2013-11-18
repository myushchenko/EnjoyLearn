using EnjoyLearn.Data.Core.Interfaces;

namespace EnjoyLearn.Web.Controllers.API
{
  using EnjoyLearn.Models;
  
  using NLog.Mvc;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Net.Http;
  using WebMatrix.WebData;

  public class UserDictionariesController : BaseApiController
  {
    private readonly IEntityRepository<UserDictionaryModel> _repository;

    public UserDictionariesController(ILogger logger, IEntityRepository<UserDictionaryModel> repository)
      : base(logger)
    {
      this._repository = repository;
    }

    //GET
    public HttpResponseMessage Get()
    {
      var learnWord = _repository.GetAll();
      var delimiters = new char[] { ',', ';','/' };
     // var dictionaryModels = learnWord as UserDictionaryModel[] ?? learnWord.ToArray();
      var lstWords = new List<DictionaryModel>();
      foreach (var dictionaryModel in learnWord)
      {
        var word = dictionaryModel.Dictionary;
        if (word.RussianWord != null)
        {
          var words = word.RussianWord.Split(delimiters);
          if (words.Count() > 2)
          {
            word.RussianWord = words[0];
          }
        }
        lstWords.Add(word);
      }
      return Request.CreateResponse(HttpStatusCode.OK, lstWords);
    }

    // POST
    public HttpResponseMessage Post(UserDictionaryModel userDictionarWord)
    {

      if (ModelState.IsValid)
      {
        userDictionarWord.Id = Guid.NewGuid();
        userDictionarWord.UserId = WebSecurity.CurrentUserId;
        _repository.Add(userDictionarWord);
        _repository.Save();
        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, userDictionarWord);
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
      //_dictionaryService.Update(model);
      return Request.CreateResponse(HttpStatusCode.NoContent);
    }

    // DELETE 
    public HttpResponseMessage Delete(Guid id)
    {
      var word = _repository.GetAll().FirstOrDefault(d => d.DictionaryId == id);
      if (word == null)
      {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      _repository.Delete(word);

      return Request.CreateResponse(HttpStatusCode.OK, word);
    }
  }
}
