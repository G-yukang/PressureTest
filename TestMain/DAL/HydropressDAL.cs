using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMain.Interfaces;
using TestMain.Model;

namespace TestMain.Interfaces
{
    internal class HydropressDAL
    {
        public async Task HydropressUpdate(HydropressModel hydropressModel)
        {
            try
            {
                var filterById = Builders<HydropressModel>.Filter.Eq("_id", "1");

                var update = Builders<HydropressModel>.Update
                    .Set(model => model.Pressurestart, hydropressModel.Pressurestart)
                    .Set(model => model.PressureOperation, hydropressModel.PressureOperation)
                    .Set(model => model.PressureClose, hydropressModel.PressureClose)
                    .Set(model => model.PressureMoldingFront, hydropressModel.PressureMoldingFront)
                    .Set(model => model.PressureMolding, hydropressModel.PressureMolding)
                    .Set(model => model.PressureOpenmould1, hydropressModel.PressureOpenmould1)
                    .Set(model => model.PressureOpenmould2, hydropressModel.PressureOpenmould2)
                    .Set(model => model.PressureOpenmould3, hydropressModel.PressureOpenmould3)
                    .Set(model => model.PressureOpenmould4, hydropressModel.PressureOpenmould4);

                var result = await MongoDBHelper.UpdateDocumentAsync("Alioth", "Hydropress", filterById, update);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task HydropressInsert(HydropressModel hydropressModel)
        {
            try
            {
                var result = MongoDBHelper.InsertDocumentAsync("Alioth", "Hydropress", hydropressModel);
            }
            catch (Exception)
            {

                throw;
            }

            return Task.CompletedTask;
        }

        public Task HydropressSelect()
        {
            try
            {
                var filterById = Builders<HydropressModel>.Filter.Eq("_id", "1");

                var result = MongoDBHelper.FindDocumentAsync("Alioth", "Hydropress", filterById);
            }
            catch (Exception)
            {

                throw;
            }

            return Task.CompletedTask;
        }
    }
}
