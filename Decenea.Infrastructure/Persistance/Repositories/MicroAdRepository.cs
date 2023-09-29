using System.Text;
using Dapper;
using Decenea.Application.Abstractions.DataAccess;
using Decenea.Application.Abstractions.Persistance.IRepositories;
using Decenea.Common.DataTransferObjects.Advertisement;

namespace Decenea.Infrastructure.Persistance.Repositories;

public class MicroAdRepository : IMicroAdRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public MicroAdRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async ValueTask<MicroAdDto> GetMicroAdDtoByMicroAdId(string microAdId, string? userId = null)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var strBuilder = new StringBuilder();
        var parameters = new DynamicParameters();
        parameters.Add("microAdId", microAdId);
        if (userId is null)
        {
            strBuilder.Append("""
                              SELECT
                                  ma."Title",
                                  ma."Description",
                                  ma."ContactPhone",
                                  ma."ContactEmail",
                                  u."UserName",
                                  ma."CityId",
                                  ma."UserId"
                              FROM "MicroAds" ma
                              JOIN "Users" u ON ma."UserId" = u."Id"
                              WHERE ma."Id" = @microAdId
                              """);
        }
        else
        {
            parameters.Add("userId", userId);
            strBuilder.Append("""
                              SELECT
                                  ma."Title",
                                  ma."Description",
                                  ma."ContactPhone",
                                  ma."ContactEmail",
                                  u."UserName",
                                  ma."CityId",
                                  ma."UserId"
                              FROM "MicroAds" ma, "Users" u
                              WHERE ma."Id" = @microAdId AND u."Id" = @userId
                              """);
        }

        var microAdDto = await connection
            .QueryFirstOrDefaultAsync<MicroAdDto>(strBuilder.ToString(), parameters);

        if (microAdDto == null)
        {
            throw new Exception("Data not found");
        }

        return microAdDto;
    }
    
    public async ValueTask<IEnumerable<MicroAdDto>> GetManyMicroAdDtos(int countryId,
        string? cityId = null,
        string? communityId = null,
        string? municipalUnitId = null,
        string? municipalityId = null,
        string? regionalUnitId = null,
        string? regionId = null)
    {
        using var connection = await _sqlConnectionFactory.CreateConnectionAsync();
        var strBuilder = new StringBuilder();
        var parameters = new DynamicParameters();
        parameters.Add("countryId", countryId);
        
        strBuilder.Append($"""
                          SELECT 
                              ma."Title",
                              ma."Description",
                              ma."ContactPhone",
                              ma."ContactEmail",
                              u."UserName",
                              ma."CityId",
                              ma."UserId"
                          FROM "MicroAds" ma
                          JOIN "Users" u ON ma."UserId" = u."Id"
                          JOIN "Cities" c ON ma."CityId" = c."Id"
                          WHERE c."CountryId" = @countryId
                          """);
        
        if (cityId is not null)
        {
            parameters.Add("cityId", cityId);

            strBuilder.Append("""
                               
                               AND ma."CityId" = @cityId
                               """);
        }
        if (communityId is not null)
        {
            parameters.Add("communityId", communityId);

            strBuilder.Append("""

                               AND c."CommunityId" = @communityId
                               """);
        }
        if (municipalUnitId is not null)
        {
            parameters.Add("municipalUnitId", municipalUnitId);

            strBuilder.Append("""

                               AND c."MunicipalUnitId" = @municipalUnitId
                               """);
        }
        if (municipalityId is not null)
        {
            parameters.Add("municipalityId", municipalityId);

            strBuilder.Append("""

                               AND c."MunicipalityId" = @municipalityId
                               """);
        }
        if (regionalUnitId is not null)
        {
            parameters.Add("regionalUnitId", regionalUnitId);

            strBuilder.Append("""

                               AND c."RegionalUnitId" = @regionalUnitId
                               """);
        }
        if (regionId is not null)
        {
            parameters.Add("regionId", regionId);

            strBuilder.Append("""

                               AND c."RegionId" = @regionId
                               """);
        }

        var microAdDtos = await connection.QueryAsync<MicroAdDto>(strBuilder.ToString(), parameters);

        return microAdDtos;
    }
}