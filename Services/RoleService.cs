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
    public class RoleService:IRoleService
    {
        private readonly IUnityOfWork unityOfWork;
        private readonly IRoleRepository roleRepository;
        public RoleService(IUnityOfWork unityOfWork,IRoleRepository roleRepository){
            this.unityOfWork = unityOfWork;
            this.roleRepository = roleRepository;
        }
        public async Task<GenericResponse<Role>> DeleteAsync(int id)
        {
            var isExist = await roleRepository.FindByIdAsync(id);
            if(isExist == null)
                return new GenericResponse<Role>("Role doesn't exist!");

            try{
                roleRepository.Delete(isExist);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<Role>(isExist);
            }
            catch(Exception ex){
                return new GenericResponse<Role>($"Error with deleting role: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await roleRepository.GetAllAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await roleRepository.FindByIdAsync(id);
        }

        public async Task<GenericResponse<Role>> SaveAsync(Role role)
        {
            try{
                await roleRepository.AddAsync(role);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<Role>(role);
            }
            catch(Exception ex){
                return new GenericResponse<Role>($"Error while saving role. Message:{ex.Message}");
            }
        }

        public async Task<GenericResponse<Role>> UpdateAsync(Role role)
        {
            var isExist = await roleRepository.FindByIdAsync(role.Id);
            if (isExist == null)
                return new GenericResponse<Role>("Role not found!");
            
            try
            {
                roleRepository.Update(role);
                await unityOfWork.CompleteAsync();
                return new GenericResponse<Role>(role);
            }
            catch (Exception ex)
            {
                return new GenericResponse<Role>($"Error when updating role: {ex.Message}");
            }
        }
    }
}