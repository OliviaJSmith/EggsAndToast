
using EggsAndToast.Web.Models;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Api;
using IntelliTect.Coalesce.Api.Behaviors;
using IntelliTect.Coalesce.Api.Controllers;
using IntelliTect.Coalesce.Api.DataSources;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Mapping.IncludeTrees;
using IntelliTect.Coalesce.Models;
using IntelliTect.Coalesce.TypeDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EggsAndToast.Web.Api
{
    [Route("api/User")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class UserController
        : BaseApiController<EggsAndToast.Data.Models.User, UserParameter, UserResponse, EggsAndToast.Data.AppDbContext>
    {
        public UserController(CrudContext<EggsAndToast.Data.AppDbContext> context) : base(context)
        {
            GeneratedForClassViewModel = context.ReflectionRepository.GetClassViewModel<EggsAndToast.Data.Models.User>();
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public virtual Task<ItemResult<UserResponse>> Get(
            string id,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<EggsAndToast.Data.Models.User> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize]
        public virtual Task<ListResult<UserResponse>> List(
            [FromQuery] ListParameters parameters,
            IDataSource<EggsAndToast.Data.Models.User> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize]
        public virtual Task<ItemResult<int>> Count(
            [FromQuery] FilterParameters parameters,
            IDataSource<EggsAndToast.Data.Models.User> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        [Authorize]
        public virtual Task<ItemResult<UserResponse>> Save(
            [FromForm] UserParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<EggsAndToast.Data.Models.User> dataSource,
            IBehaviors<EggsAndToast.Data.Models.User> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("save")]
        [Consumes("application/json")]
        [Authorize]
        public virtual Task<ItemResult<UserResponse>> SaveFromJson(
            [FromBody] UserParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<EggsAndToast.Data.Models.User> dataSource,
            IBehaviors<EggsAndToast.Data.Models.User> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("bulkSave")]
        [Authorize]
        public virtual Task<ItemResult<UserResponse>> BulkSave(
            [FromBody] BulkSaveRequest dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<EggsAndToast.Data.Models.User> dataSource,
            [FromServices] IDataSourceFactory dataSourceFactory,
            [FromServices] IBehaviorsFactory behaviorsFactory)
            => BulkSaveImplementation(dto, parameters, dataSource, dataSourceFactory, behaviorsFactory);

        // Methods from data class exposed through API Controller.

        /// <summary>
        /// Method: GetPhoto
        /// </summary>
        [HttpGet("GetPhoto")]
        [Authorize]
        public virtual async Task<ActionResult<ItemResult<IntelliTect.Coalesce.Models.IFile>>> GetPhoto(
            [FromServices] IDataSourceFactory dataSourceFactory,
            [FromQuery] string id,
            [FromQuery] byte[] etag)
        {
            var _params = new
            {
                Id = id,
                Etag = etag ?? await ((await Request.ReadFormAsync()).Files[nameof(etag)]?.OpenReadStream().ReadAllBytesAsync(true) ?? Task.FromResult<byte[]>(null))
            };

            var dataSource = dataSourceFactory.GetDataSource<EggsAndToast.Data.Models.User, EggsAndToast.Data.Models.User>("Default");
            var itemResult = await dataSource.GetItemAsync(_params.Id, new DataSourceParameters());
            if (!itemResult.WasSuccessful)
            {
                return new ItemResult<IntelliTect.Coalesce.Models.IFile>(itemResult);
            }
            var item = itemResult.Object;

            var _currentVaryValue = item.PhotoHash;
            if (_currentVaryValue != default)
            {
                var _expectedEtagHeader = new Microsoft.Net.Http.Headers.EntityTagHeaderValue('"' + Microsoft.AspNetCore.WebUtilities.Base64UrlTextEncoder.Encode(_currentVaryValue) + '"');
                var _cacheControlHeader = new Microsoft.Net.Http.Headers.CacheControlHeaderValue { Private = true, MaxAge = TimeSpan.Zero };
                if (etag != default && _currentVaryValue.SequenceEqual(etag))
                {
                    _cacheControlHeader.MaxAge = TimeSpan.FromDays(30);
                }
                Response.GetTypedHeaders().CacheControl = _cacheControlHeader;
                Response.GetTypedHeaders().ETag = _expectedEtagHeader;
                if (Request.GetTypedHeaders().IfNoneMatch.Any(value => value.Compare(_expectedEtagHeader, true)))
                {
                    return StatusCode(StatusCodes.Status304NotModified);
                }
            }

            var _methodResult = item.GetPhoto(
                User,
                Db
            );
            if (_methodResult.Object != null)
            {
                return File(_methodResult.Object);
            }
            var _result = new ItemResult<IntelliTect.Coalesce.Models.IFile>(_methodResult);
            _result.Object = _methodResult.Object;
            return _result;
        }
    }
}
