using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Errors.Model;
using WorkBook.DTOs;
using WorkBook.Models;
using WorkBook.WorkBookDbModel;

namespace WorkBook.repos
{
    public class DbServices
    {
        private DbModel context = new DbModel();
        public DbServices()
        {
            context.Database.EnsureCreated();
        }

        public void AddRecord(WorkersModel worker)
        {
            context.Worker.Add(worker);
            int x = context.SaveChanges();
        }
        public void AddRecord(WorkerAuthModel workerAuth)
        {
            context.WorkerAuth.Add(workerAuth);
            int x = context.SaveChanges();
        }
        public void AddRecord(CustomerAuthModel customerAuth)
        {
            context.CustomerAuth.Add(customerAuth);
            int x = context.SaveChanges();
        }
        public void AddRecord(CustomerModel customer)
        {
            context.Customer.Add(customer);
            int x = context.SaveChanges();
        }
        public void AddRecord(WorkerProfessionModel workerProfssion)
        {
            context.WorkerProfession.Add(workerProfssion);
            int x = context.SaveChanges();
        }
        public void AddRecord(RatingModel rating)
        {
            context.Rating.Add(rating);
            int x = context.SaveChanges();
        }
        public void AddRecord(ProjectModel project)
        {
            context.Project.Add(project);
            int x = context.SaveChanges();
        }
        public void AddRecord(ProfessionModel profession)
        {
            context.Profession.Add(profession);
            int x = context.SaveChanges();
        }
        public void AddRecord(CommentModel comment)
        {
            context.Comment.Add(comment);
            int x = context.SaveChanges();
        }
        public bool CheckWorkerByEmail(string email)
        {
            var exist = context.Worker.Any(x => x.Email == email);
            return exist;
        }
        public WorkersModel? GetWorker(string email)
        {
            var worker = context.Worker.
                Include(a=>a.WorkerAuth).
                FirstOrDefault(x => x.Email == email);
            return worker;
        }
        public void WorkerSignIn(WorkersModel worker)
        {
            //worker.WorkerAuth.UpdatedAt = DateTime.Now;
            context.SaveChanges();
        }
        public void CustomerSignIn(CustomerModel customer)
        {
            //customer.CustomerAuth.UpdatedAt = DateTime.Now;
            context.SaveChanges();
        }
        public WorkersModel? GetWorkerById(int workerId)
        {
            var worker = context.Worker.FirstOrDefault(x => x.Id == workerId);
            return worker;
        }
        public WorkersModel? GetWorkerWithProfessionsById(int workerId)
        {
            var worker = context.Worker.
                Include(w=>w.WorkerProfessions).
                FirstOrDefault(x => x.Id == workerId);
            return worker;
        }
        public WorkersModel? GetWorkerByToken(string token)
        {
            var worker = context.Worker.Where(t => t.WorkerAuth.Token == token).FirstOrDefault();
            return worker;
        }

        public void UpdateWorkerProfile(int workerId, EditWorkerReq req)
        {
            var worker = GetWorkerWithProfessionsById(workerId);
            if (worker == null)
            {
                throw new NotFoundException("this worker is not exist");
            }
            

            worker.Address1 = req.address_1;
            worker.Address2 = req.address_2;
            worker.Address3 = req.address_3;
            worker.FirestName = req.first_name;
            worker.LastName = req.last_name;
            worker.DateOfBirth = req.date_of_birth;
            worker.WorkerProfessions.Clear();
            foreach(var professionId in req.profession_ids)
            {
                worker.WorkerProfessions.Add(new WorkerProfessionModel() { WorkerId = workerId, ProfessionId = professionId });
            }

            context.SaveChanges();
            
        }

        public void UpdateWorkerImg(int workerId, byte[]? img)
        {
            var worker = GetWorkerById(workerId);
            if (worker == null)
            {
                throw new KeyNotFoundException("this Id is not available");
            }
            worker.Img = img;
            int x = context.SaveChanges();
        }

        public CustomerModel? GetCustomerById(int workerId)
        {
            var customer = context.Customer.FirstOrDefault(x => x.Id == workerId);
            return customer;
        }
        public bool CheckCustomerByEmail(string email)
        {
            var exist = context.Customer.Any(x => x.Email == email);
            return exist;
        }
        public void UpdateCustomerImg(int workerId, byte[]? img)
        {
            var customer = GetCustomerById(workerId);
            if (customer == null)
            {
                throw new KeyNotFoundException("this Id is not available");
            }
            customer.Img = img;
            int x = context.SaveChanges();
        }
        public List<ProfessionRes> GetProfessionsByWorkerId(int workerId)
        {

            var profession = from pi in context.WorkerProfession
                             join
                             p in context.Profession on pi.ProfessionId equals p.Id
                             where pi.WorkerId == workerId
                             select new ProfessionRes(pi.ProfessionId, p.Name);
            return profession.ToList();

        }

        public CustomerModel? GetCustomer(string email)
        {
            var customer = context.Customer.FirstOrDefault(x => x.Email == email);
            return customer;
        }
        public CustomerModel? GetCustomerByToken(string token)
        {
            var customer = context.Customer.Where(t => t.CustomerAuth.Token == token).FirstOrDefault();
            return customer;
        }

        public WorkersModel CreateWorker(WorkersModelReq req, string token)
        {
            
            var worker = new WorkersModel()
            {
                FirestName = req.first_name,
                LastName = req.last_name,
                Email = req.email,
                Password = req.password,
                Address1 = req.address_1,
                Address2 = req.address_2,
                Address3 = req.address_3,
                PhoneNumber = req.phone,
                DateOfBirth = req.date_of_birth,
                
            };
            return worker;
        }


        public CustomerModel CreateCustomer(CustomerModelReq req, string token)
        {
            
            var customer = new CustomerModel()
            {
                FirstName = req.first_name,
                LastName = req.last_name,
                Email = req.email,
                Password = req.password,
                Address1 = req.address_1,
                Address2 = req.address_2,
                Address3 = req.address_3,
                PhoneNumber = req.phone,
                HomePhoneNumber = req.home_phone,
                DateOfBirth = req.date_of_birth,
                

            };

            return customer;
        }


        public bool CheckProjectById(int projectId)
        {
            
            var exist = context.Project.Any(p => p.Id == projectId);
            return exist;
        }

        public WorkerProfessionModel? GetWorkerProfessionByIds(int workerId, int professionId)
        {
            var workerprofession = context.WorkerProfession.FirstOrDefault(x => (x.WorkerId == workerId && x.ProfessionId == professionId));
            return workerprofession;

        }
        public ProfessionModel? GetProfessionById(int professionId)
        {
            var profession = context.Profession.FirstOrDefault(x => x.Id == professionId);
            return profession;
        }

        public ProjectModel CreateProject(CustomerModel customer, CreateProjectReq req, WorkerProfessionModel workerProfession)
        {
            
            var project = new ProjectModel()
            {
                Title = req.title,
                Description = req.description,
                StartDate = req.start_date,
                EndDate = req.end_date,
                DurationDays = req.duration_days,
                Price = req.price,
                Status = req.status,
                Address = req.address,
                WorkerProfession = workerProfession,
                Customer = customer,
                Comments = new List<CommentModel>()
            };

            return project;
        }

        public ProjectModel? GetProjectByProjectIdAndCustomerId(int projectId, int customerId)
        {
            var project = context.Project.Include(p=>p.WorkerProfession).FirstOrDefault(p=>p.Id == projectId && p.Customer.Id == customerId);
            return project;
        }
        public ProjectModel? GetProjectByProjectIdAndWorkerId(int projectId, int workerId)
        {
            var project = context.Project.
                Include(p=>p.WorkerProfession).
                FirstOrDefault(p => p.Id == projectId && p.WorkerProfession.WorkerId == workerId);
            return project;
        }

        public void UpdateProject(ProjectModel project, EditProjectReq req, WorkerProfessionModel workerProfession)
        {
            project.Title = req.title;
            project.Description = req.description;
            project.StartDate = req.start_date;
            project.EndDate = req.end_date;
            project.DurationDays = req.duration_days;
            project.Price = req.price;
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.WriteLine(req.status);
            project.Status = req.status;
            project.Address = req.address;
            project.WorkerProfession = workerProfession;

            context.SaveChanges();
            
        }

        public List<ProjectModel> GetProjectsByCustomerId(int customerId)
        {
            var projects = context.Project.Include(p=>p.WorkerProfession).Where(p=> p.CustomerId == customerId).ToList();
            return projects;
        }
        
        public List<ProjectModel> GetProjectsByWorkerId(int workerId)
        {
            var projects = context.Project.
                Include(p=>p.WorkerProfession).
                Where(p => p.WorkerProfession.WorkerId == workerId).ToList();
            return projects;
        }
        public List<ProjectResp> CreateProjectResp(List<ProjectModel> projects)
        {
            var projectResp = new List<ProjectResp>();
            foreach (var project in projects)
            {
                var resp = new ProjectResp(project.Id, project.Title, project.Description, project.StartDate, project.EndDate,
                    project.DurationDays, project.Price, project.WorkerProfession.ProfessionId, project.Status, project.Address);
                    
                projectResp.Add(resp);
            }
            return projectResp;
        }

        public CommentModel CreateCustomerComment(string value, int customerId, int projectId)
        {
            var comment = new CommentModel()
            {
                Comment = value,
                CustomerId = customerId,
                ProjectId = projectId,
                WorkerId = null
            };
            return comment;

        }

        public WorkerProfileResp CreateWorkerProfileResp(WorkersModel worker)
        {
            var workerProfileResp = new WorkerProfileResp()
            {
                first_name = worker.FirestName,
                last_name = worker.LastName,
                address_1 = worker.Address1,
                address_2 = worker.Address2,
                address_3 = worker.Address3,
                date_of_birth = worker.DateOfBirth,
                profession_ids = worker.WorkerProfessions.Select(p => p.ProfessionId).ToList()

            };
            return workerProfileResp;
        }

        public CustomerProfileResp CreateCustomerProfileResp(CustomerModel customer)
        {
            var customerProfileResp = new CustomerProfileResp()
            {
                first_name = customer.FirstName,
                last_name = customer.LastName,
                address_1 = customer.Address1,
                address_2 = customer.Address2,
                address_3 = customer.Address3,
                date_of_birth = customer.DateOfBirth,

            };
            return customerProfileResp;
        }

        public void UpdateCustomerProfile(int customerId, EditCustomerReq req)
        {
            var customer = GetCustomerById(customerId);
            if (customer == null)
            {
                throw new NotFoundException("this customer is not exist");
            }

            customer.Address1 = req.address_1;
            customer.Address2 = req.address_2;
            customer.Address3 = req.address_3;
            customer.FirstName = req.first_name;
            customer.LastName = req.last_name;
            customer.DateOfBirth = req.date_of_birth;
            
            context.SaveChanges();

        }

        public CommentModel CreateWorkerComment(string value, int workerId, int projectId)
        {
            var comment = new CommentModel()
            {
                Comment = value,
                WorkerId = workerId,
                ProjectId = projectId
            };
            return comment;
        }

        public List<CommentModel> GetComments(int projectId)
        {
            var comments = context.Comment.
                Include(p => p.Project).
                Include(c=> c.Customer).
                Include(w=>w.Worker).
                Where(p => p.ProjectId == projectId).ToList();
            return comments;
        }

        public List<CommentResp> CreateCommentResp(IEnumerable<CommentModel> comments)
        {
            var resp = new List<CommentResp>();
            foreach (var comment in comments)
            {
                string username = "";
                if(comment.CustomerId != null)
                {
                    username = comment.Customer.FirstName + " " + comment.Customer.LastName;
                }
                else if(comment.WorkerId != null)
                {
                    username = comment.Worker.FirestName + " " + comment.Worker.LastName;
                }
                var comm = new CommentResp(comment.Id, username, comment.Comment);
                resp.Add(comm);
            }
            return resp;
        }

        public RatingModel CreateRate(int customerId, int projectId,int workerProfessionId, RateReq req)
        {
            var rate = new RatingModel()
            {
                CustomerId = customerId,
                ProjectId = projectId,
                Rating = req.value,
                Description = req.description,
                WorkerProfessionId = workerProfessionId,
            };
            return rate;
        }

        public List<ProfessionWorkers> GetWorkersForProfession()
        {
            
            var result = context.Profession.Select(p => new ProfessionWorkers()
            {
                id = p.Id,
                name = p.Name,
                workers = context.WorkerProfession
                    .Where(wp => wp.ProfessionId == p.Id)
                    .Select(wp => new WorkersForProfessionRes()
                    {
                        id = wp.Worker.Id,
                        name = wp.Worker.FirestName + " " + wp.Worker.LastName
                    })
                    .ToList()
            }).ToList();


            return result;
        }

        public List<TopProfessionWorkers> GetTopProfessionWorkers()
        {
            
            var topProfession = context.WorkerProfession.
                Include(wp => wp.Worker).
                Include(wp => wp.Profession).
                Include(wp => wp.Rating).
                GroupBy(wp => new
                {
                    wp.Profession.Id,
                    wp.Profession.Name
                }).Select(g => new TopProfessionWorkers()
                {
                    id = g.Key.Id,
                    name = g.Key.Name,
                    Workers = g.Select(wp => new TopWorkersForProfessionRes()
                    {
                        id = wp.Worker.Id,
                        name = wp.Worker.FirestName + " " + wp.Worker.LastName,
                        avg_rate = wp.Rating.Select(r => r.Rating).Average(),
                        descriptions = wp.Rating.Select(r=> r.Description).ToList()

                    }).OrderByDescending(w=>w.avg_rate).ToList()
                }).ToList();

            return topProfession;
                
                

        }



    }         
}
