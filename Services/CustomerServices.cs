using SendGrid.Helpers.Errors.Model;
using WorkBook.DTOs;
using WorkBook.Models;
using WorkBook.repos;

namespace WorkBook.Services
{
    public class CustomerServices
    {
        DbServices _db;

        public CustomerServices()
        {
            _db = new DbServices();
        }

        private bool IsExist(string email)
        {
            var exist = _db.CheckCustomerByEmail(email);

            return exist;
        }


        public void SignUp(CustomerModelReq req)
        {
            //need validation here
            var exist = IsExist(req.email);
            if (exist)
            {
                throw new InvalidOperationException("Email already exist");
            }
            var token = Guid.NewGuid().ToString();

            var customer = _db.CreateCustomer(req, token);
            _db.AddRecord(customer);
        }

        public SignInResp SignIn(string email, string password)
        {
            var customer = _db.GetCustomer(email);
            if (customer == null || customer.Password != password)
            {
                throw new UnauthorizedAccessException("the Email or password is wrong");
            }
            _db.CustomerSignIn(customer);
            var resp = new SignInResp("ythhrythr", $"{customer.Id}");
            return resp;
        }

        public CustomerModel GetCurrentCustomer(string token)
        {
            var customer = _db.GetCustomerByToken(token);
            if (customer == null)
            {
                throw new UnauthorizedAccessException("this user is not registered pleas signin first or signup if you don't have account");

            }
            return customer;
        }

        public void UpdateCustomerImg(int customerId, byte[]? imgData)
        {

            
            _db.UpdateCustomerImg(customerId, imgData);
            return;
        }

        public byte[]? GetCustomerImg(int customerId)
        {
            
            var custimer = _db.GetCustomerById(customerId);
            if (custimer == null)
            {
                throw new KeyNotFoundException("this id is not valid");
            }
            return custimer.Img;
        }

        public void UpdateCustomerProfile(int workerId, EditCustomerReq req)
        {
            _db.UpdateCustomerProfile(workerId, req);
            return;
        }

        public CustomerProfileResp GetCustomerProfile(int customerId)
        {
            var customer = _db.GetCustomerById(customerId);
            if (customer == null)
            {
                throw new NotFoundException("this worker is not exist");
            }
            var workerProfile = _db.CreateCustomerProfileResp(customer);

            return workerProfile;

        }

        public void RateWorker(int customerId, int projectId, RateReq req)
        {
            var project = _db.GetProjectByProjectIdAndCustomerId(projectId, customerId);

            var rate = _db.CreateRate(customerId, projectId, project.WorkerProfessionId, req);
            _db.AddRecord(rate);
        }

    }
}
