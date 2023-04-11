using Container.Models;
using DB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katech_Task.Services
{
    public interface ITrademarkService
    {
        Task<BaseResponse<List<Trademark>>> GetMarks(string word);
    }

    public class TrademarkService : ITrademarkService
    {

        private readonly ILogger<TrademarkService> _log;

        public TrademarkService(ILogger<TrademarkService> logger)
        {
            _log = logger;
        }

        public async Task<BaseResponse<List<Trademark>>> GetMarks(string word)
        {
            BaseResponse<List<Trademark>> response = new BaseResponse<List<Trademark>>();
            _log.LogInformation("GetMarks method started and parameter:", word);

            try
            {
                if (string.IsNullOrEmpty(word))
                {
                    response.Data = null;
                    response.Code = Container.Enums.ResultCode.BadRequest;
                    response.Description = Container.Enums.ResultCode.BadRequest.GetDescription();

                    return response;
                }

                var data = await MSDB.GetTrademarksAsync(word);

                _log.LogInformation("Response GetTrademarksAsync:", data?.ToStringArray());

                if (data.Count > 0)
                {
                    response.Data = data;
                    response.Code = Container.Enums.ResultCode.Ok;
                    response.Description = Container.Enums.ResultCode.Ok.GetDescription();
                }
                else
                {

                    response.Data = null;
                    response.Code = Container.Enums.ResultCode.NotFound;
                    response.Description = Container.Enums.ResultCode.NotFound.GetDescription();
                }

            }
            catch (Exception exp)
            {
                _log.LogError(exp.Message, exp);
                response.Data = null;
                response.Code = Container.Enums.ResultCode.UnknownError;
                response.Description = Container.Enums.ResultCode.UnknownError.GetDescription();
            }

            return response;
        }
    }

}
