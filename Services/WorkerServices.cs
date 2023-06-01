using WorkBook.DTOs;
using WorkBook.Models;
using WorkBook.repos;
using SendGrid.Helpers.Errors.Model;

namespace WorkBook.Services
{
    public class WorkerServices
    {
        DbServices _db;
        public WorkerServices()
        {
            _db = new DbServices();
        }

        private bool IsExist(string email)
        {
            
            var exist = _db.CheckWorkerByEmail(email);

            return exist;
        }


        public void SignUp(WorkersModelReq req)
        {
            var exist = IsExist(req.email);
            if (exist)
            {
                throw new InvalidOperationException("Email already exist");
            }
            var token = Guid.NewGuid().ToString();

            var worker = _db.CreateWorker(req, token);
            _db.AddRecord(worker);

            foreach (var id in req.profession_ids)
            {
                var workerProfession = new WorkerProfessionModel() { ProfessionId = id, WorkerId = worker.Id };
                _db.AddRecord(workerProfession);
            }
            return;
        }

        public SignInResp SignIn(string email, string password)
        {
            var worker = _db.GetWorker(email);
            if (worker == null || worker.Password != password)
            {
                throw new UnauthorizedAccessException("the Email or password is wrong");
            }
            _db.WorkerSignIn(worker);
            var resp = new SignInResp("ejwsfc", $"{worker.Id}");
            return resp;
        }

        public WorkersModel GetCurrentWorker(string token)
        {
            var worker = _db.GetWorkerByToken(token);
            if (worker == null)
            {
                throw new UnauthorizedAccessException("this user is not registered pleas signin first or signup if you don't have account");

            }
            return worker;
        }

        public void UpdateWorkerImg(int workerId, byte[]? imgData)
        {

            _db.UpdateWorkerImg(workerId, imgData);
            return;
        }

        public byte[]? GetWorkerImg(int workerId)
        {
            var worker = _db.GetWorkerById(workerId);
            if (worker == null)
            {
                throw new KeyNotFoundException("this id is not valid");
            }
            return worker.Img;
        }

        public void UpdateWorkerProfile(int workerId, EditWorkerReq req)
        {
            _db.UpdateWorkerProfile(workerId, req);
            return;
        }

        public WorkerProfileResp GetWorkerProfile(int workerId)
        {
            var worker = _db.GetWorkerWithProfessionsById(workerId);
            if(worker == null)
            {
                throw new NotFoundException("this worker is not exist");
            }
            var workerProfile = _db.CreateWorkerProfileResp(worker);
            
            return workerProfile;

        }

    }
}
