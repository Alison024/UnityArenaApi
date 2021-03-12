using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityArenaApi.Domain.IRepositories;
using UnityArenaApi.Domain.IServices;
using UnityArenaApi.Domain.Models;
using UnityArenaApi.Domain.Responses;
namespace UnityArenaApi.Services
{
    public class ModeService:IModeService
    {
        private readonly IUnityOfWork unityOfWork;
        private readonly IModeRepository modeRepository;
        public ModeService(IUnityOfWork unityOfWork,IModeRepository modeRepository){
            this.unityOfWork = unityOfWork;
            this.modeRepository = modeRepository;
        }

        public async Task<GenericResponse<Mode>> DeleteAsync(int id)
        {
            var existMode = await modeRepository.FindByIdAsync(id);
            if(existMode == null)
                return new GenericResponse<Mode>("Mode doesn't exist!");
            try{
                modeRepository.Delete(existMode);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<Mode>(existMode);
            }
            catch(Exception ex){
                return new GenericResponse<Mode>($"Error with deleting mode: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Mode>> GetAllAsync()
        {
            return await modeRepository.GetAllAsync();
        }

        public async Task<Mode> GetById(int id)
        {
            return await modeRepository.FindByIdAsync(id);
        }

        public async Task<GenericResponse<Mode>> SaveAsync(Mode mode)
        {
            try{
                await modeRepository.AddAsync(mode);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<Mode>(mode);
            }
            catch(Exception ex){
                return new GenericResponse<Mode>($"Error while saving mode. Message:{ex.Message}");
            }
        }

        public async Task<GenericResponse<Mode>> UpdateAsync(Mode mode)
        {
            var existMode = await modeRepository.FindByIdAsync(mode.Id);
            if (existMode == null)
                return new GenericResponse<Mode>("Mode not found!");
            
            try
            {
                modeRepository.Update(mode);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<Mode>(mode);
            }
            catch (Exception ex)
            {
                return new GenericResponse<Mode>($"Error when updating mode: {ex.Message}");
            }
        }
    }
}