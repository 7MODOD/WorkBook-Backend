using SendGrid.Helpers.Errors.Model;
using WorkBook.DTOs;
using WorkBook.Models;
using WorkBook.repos;

namespace WorkBook.Services
{
    public class ProjectServices
    {
        private DbServices _db;
        public ProjectServices()
        {
            _db = new DbServices();
        }
        private bool IsExist(int projectId)
        {
            
            return _db.CheckProjectById(projectId);
        }

        public void CreateProject(int customerId, CreateProjectReq req)
        {
            
            var worker = _db.GetWorkerById(req.worker_id);
            if (worker == null)
            {
                throw new NotFoundException("this worker is not exist");
            }
            var workerprofession = _db.GetWorkerProfessionByIds(req.worker_id, req.profession_id);
            if (workerprofession == null)
            {
                var profession = _db.GetProfessionById(req.profession_id);
                if (profession == null)
                {
                    throw new NotFoundException("this profession is not exist");
                }
                workerprofession = new WorkerProfessionModel() { Worker = worker, Profession = profession };
                _db.AddRecord(workerprofession);
            }
            var customer = _db.GetCustomerById(customerId);
            if (customer == null)
            {
                throw new NotFoundException("this customer is not exist");
            }


            var project = _db.CreateProject(customer, req, workerprofession);
            _db.AddRecord(project);

        }

        public void UpdateProjectByCustomer(int projectId, int customerId, EditProjectReq req)
        {
            var project = _db.GetProjectByProjectIdAndCustomerId(projectId, customerId);
            if (project == null)
            {
                throw new NotFoundException("this project is not exist");
            }
            Console.WriteLine(project.WorkerProfession.WorkerId);
            Console.WriteLine(req.profession_id);
            
            var workerProfession = _db.GetWorkerProfessionByIds(project.WorkerProfession.WorkerId, req.profession_id);
            if (workerProfession == null)
            {
                Console.WriteLine("there is an errorxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            }
            _db.UpdateProject(project, req, workerProfession);
        }

        public void UpdateProjectByWorker(int projectId, int workerId, EditProjectReq req)
        {
            var project = _db.GetProjectByProjectIdAndWorkerId(projectId, workerId);
            if (project == null)
            {
                throw new NotFoundException("this project is not exist");
            }
            var workerProfession = _db.GetWorkerProfessionByIds(workerId, req.profession_id);
            if (workerProfession == null)
            {
                throw new ArgumentException("this worker doesn't have this profession");
            }
            _db.UpdateProject(project, req, workerProfession);

        }

        public IEnumerable<ProjectResp> GetProjectsByCustomerId(int customerId, string? filterBy, string? order , string? status)
        {
            var projects = _db.GetProjectsByCustomerId(customerId);

            if (!projects.Any())
            {
                throw new NotFoundException();
            }

            var resps = _db.CreateProjectResp(projects);

            var filteredProjects = FilterProjects(resps, filterBy, order, status);
            return filteredProjects;

        }

        public IEnumerable<ProjectResp> GetProjectsByWorkerId(int workerId, string? filterBy, string? order, string? status)
        {
            var projects = _db.GetProjectsByWorkerId(workerId);

            if (!projects.Any())
            {
                throw new NotFoundException();
            }

            var resps = _db.CreateProjectResp(projects);

            var filteredProjects = FilterProjects(resps, filterBy, order, status);
            return filteredProjects;

        }

        public IEnumerable<ProjectResp> FilterProjects(List<ProjectResp> projects, string? filterBy, string? order, string? status)
        {
            IEnumerable<ProjectResp> filteredProjects = projects;

            if(!string.IsNullOrEmpty(status))
            {
                string[] statusFilters = status.Split('|');
                filteredProjects = filteredProjects.Where(p => statusFilters.Contains(p.status.ToString()));
            }
            if(string.IsNullOrEmpty(filterBy)){
                return filteredProjects;
            }
            if(string.IsNullOrEmpty(order)){
                return filteredProjects;
            }

            switch (filterBy)
            {
                case "price":
                    filteredProjects = order == "ASC"
                        ? filteredProjects.OrderBy(p => p.price)
                        : filteredProjects.OrderByDescending(p => p.price);
                    break;
                case "duration_days":
                    filteredProjects = order == "ASC"
                        ? filteredProjects.OrderBy(p => p.duration_days)
                        : filteredProjects.OrderByDescending(p => p.duration_days);
                    break;
                case "start_date":
                    filteredProjects = order == "ASC"
                        ? filteredProjects.OrderBy(p => p.start_date)
                        : filteredProjects.OrderByDescending(p => p.start_date);
                    break;
                case "end_date":
                    filteredProjects = order == "ASC"
                        ? filteredProjects.OrderBy(p => p.end_date)
                        : filteredProjects.OrderByDescending(p => p.end_date);
                    break;
                //case "multipart":
                  //  filteredProjects = order == "ASC"
                    //    ? filteredProjects.OrderBy(p => p.Multipart)
                      //  : filteredProjects.OrderByDescending(p => p.Multipart);
                    //break;
                default:
                    // Handle unsupported filter_by values
                    throw new ArgumentException("Invalid filter_by value");
            }

            return filteredProjects;

        }



    }
}
