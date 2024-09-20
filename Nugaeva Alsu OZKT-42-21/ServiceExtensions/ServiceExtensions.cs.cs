using Microsoft.Extensions.DependencyInjection;
using Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces;
using System.Collections.Generic;

namespace Nugaeva_Alsu_OZKT_42_21.ServiceExtensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IStudentService, StudentService>();

			return services;
		}
	}
}
//insert into cd_group (c_group_name)
//select 'КТ-42-21';

//insert into cd_group (c_group_name)
//select 'КТ-41-21';

//insert into cd_student (c_student_firstname, c_student_lastname, c_student_middlename, f_group_id)
//select 'Ivan','Ivanov','Ivanovich',1;

//insert into cd_student (c_student_firstname, c_student_lastname, c_student_middlename, f_group_id)
//select 'Petr','Petrov','Petrovich',1;