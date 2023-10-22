using Flurl.Http;
using HHApiLib.Configurations;
using HHApiLib.Models;
using HHApiLib.Models.Resume;
using HHApiLib.Utils;

namespace HHApiLib.Apis;

public class ResumeApi : ApiBase
{
    public ResumeApi(string token) : base(token)
    {
    }

    public async Task<ArrayWrapper<Resume>> GetResumesAsync()
    {
        return await $"{Setup.Conf.MainApiUrl}/resumes/mine"
            .WithOAuthBearerToken(Token)
            .WithHeaderAgent()
            .GetJsonAsync<ArrayWrapper<Resume>>();
    }

    public async Task<Resume> GetResumeAsync(string resumeId)
    {
        return await $"{Setup.Conf.MainApiUrl}/resumes/{resumeId}"
            .WithOAuthBearerToken(Token)
            .WithHeaderAgent()
            .GetJsonAsync<Resume>();
    }

    public async Task<IFlurlResponse> Republish(string resumeId)
    {
        return await $"{Setup.Conf.MainApiUrl}/resumes/{resumeId}/publish"
            .WithOAuthBearerToken(Token)
            .WithHeaderAgent()
            .PostAsync();
    }
}
