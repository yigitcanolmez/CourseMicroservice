using Dapper;
using FreeCourse.Services.Discount.Models;
using FreeCourse.Services.Discount.Services.Constants.MessageConstants;
using FreeCourse.Services.Discount.Services.Constants.QueryConstants;
using FreeCourse.Shared.DTOs;
using Npgsql;
using System.Data;
using System.Linq;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var isAvailable = await this.GetById(id);

            if (isAvailable == null)
                return Response<NoContent>.Fail(Messages.DISCOUNT_NOT_FOUND, StatusCodes.Status400BadRequest);

            var status = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE id =@Id", new { Id = id });
            if (status > 0)
            {
                return Response<NoContent>.Success(StatusCodes.Status204NoContent);
            }
            return Response<NoContent>.Fail(Messages.DISCOUNT_DELETING_ERROR, StatusCodes.Status500InternalServerError);

        }

        public async Task<Response<List<DiscountModel>>> GetAll()
        {
            string query = Queries.SelectQuery.Replace("[%FILTER%]", "*")
                                              .Replace("[%TABLE%]", nameof(Discount));

            var discounts = await _dbConnection.QueryAsync<DiscountModel>(query);
            return Response<List<DiscountModel>>.Success(discounts.ToList(), StatusCodes.Status200OK);
        }

        public async Task<Response<DiscountModel>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<DiscountModel>("SELECT * FROM discount WHERE userid = @UserId and code = @Code", new
            {
                UserId = userId,
                Code = code
            });

            var hasDiscount = discounts.FirstOrDefault();
            if (hasDiscount != null)
            {
                return Response<DiscountModel>.Fail(Messages.DISCOUNT_DELETING_ERROR, StatusCodes.Status400BadRequest);
            }
            return Response<DiscountModel>.Success(hasDiscount, StatusCodes.Status200OK);

        }

        public async Task<Response<DiscountModel>> GetById(int id)
        {
            string query = Queries.SelectQuery.Replace("[%FILTER%]", "*")
                                              .Replace("[%TABLE%]", nameof(Discount));

            var discounts = (await _dbConnection.QueryAsync<DiscountModel>(query + " where id = @id", new { Id = id })).SingleOrDefault();

            if (discounts == null)
            {
                return Response<DiscountModel>.Fail(Messages.DISCOUNT_NOT_FOUND, StatusCodes.Status404NotFound);
            }
            return Response<DiscountModel>.Success(discounts, StatusCodes.Status200OK);

        }

        public async Task<Response<NoContent>> Save(DiscountModel discount)
        {

            var status = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid, rate, code) VALUES (@UserId, @Rate, @Code)", discount);

            if (status > 0)
            {
                return Response<NoContent>.Success(StatusCodes.Status204NoContent);
            }
            return Response<NoContent>.Fail(Messages.DISCOUNT_SAVING_ERROR, StatusCodes.Status500InternalServerError);

        }

        public async Task<Response<NoContent>> Update(DiscountModel discount)
        {
            var isAvailable = await this.GetById(discount.Id);

            if (isAvailable == null)
                return Response<NoContent>.Fail(Messages.DISCOUNT_NOT_FOUND, StatusCodes.Status400BadRequest);

            var status = await _dbConnection.ExecuteAsync("UPDATE discount SET userid = @UserId, rate = @Rate, code = @Code where id = @Id)", new
            {
                UserId = discount.UserId,
                Rate = discount.Rate,
                Code = discount.Code,
                Id = discount.Id
            });

            if (status > 0)
            {
                return Response<NoContent>.Success(StatusCodes.Status204NoContent);
            }
            return Response<NoContent>.Fail(Messages.DISCOUNT_UPDATING_ERROR, StatusCodes.Status500InternalServerError);

        }
    }
}
