using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.BL.Mappers;
using ESurvey.Common.Enums;
using ESurvey.DAL.Concrate;
using ESurvey.Entity;
using ESurvey.UIModels;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.BL.Concrete
{
    public class MatrixRowCrudLogic
    {
        public async Task<DataResult<QuestionUiModel>> Create(int parentId)
        {
            using (var holder = new RepositoryHolder())
            {
                var parent = await holder.QuestionRepository.GetByIdAsync(parentId);
                var number = await holder.QuestionRepository
                    .GetCountByAsync(q => q.Parent_Question == parentId);
                var entity = new Questions()
                {
                    Is_matrix = true,
                    Title = "Untitled",
                    QuestionType = (int)QuestionType.Matrix,
                    Parent_Question = parentId,
                    SurveyId = parent.SurveyId,
                    Number = number + 1};
                    holder.QuestionRepository.Insert(entity);
                    await holder.SaveChangesAsync();
                    var result = new QuestionMapper().EntityToUi(entity);

                    return new DataResult<QuestionUiModel>(result);
            }
        }


        public async Task<Result> Delete(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                holder.QuestionRepository.RemoveBy(q=>q.Parent_Question!=null && q.Id == id);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }


        public async Task<Result> Rename(RenameRequestUiModel model)
        {
            using (var holder = new RepositoryHolder())
            {
                var entry = await holder.QuestionRepository.GetByIdAsync(model.Id);
                entry.Title = model.Name;
                holder.QuestionRepository.Update(entry);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }

        public async Task<DataResult<List<QuestionUiModel>>> FetchQuestionRows(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                var entries = await holder.QuestionRepository.FetchByAsync(q => q.Parent_Question == id);
                var mapper = new QuestionMapper();
                var result = entries.Select(e => mapper.EntityToUi(e)).ToList();
                
                return new DataResult<List<QuestionUiModel>>(result);
            }
        }


    }
}
