import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/employee.model';
import { EmployeeserviceService } from 'src/app/services/employee.service';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css'],
})
export class EditEmployeeComponent implements OnInit {
  employeeDetails: Employee = {
    id: '',
    name: '',
    email: '',
    phone: 0,
    salary: 0,
    department: '',
  };
  constructor(
    private route: ActivatedRoute,
    private employeeserviceService: EmployeeserviceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          this.employeeserviceService.getEmployee(id).subscribe({
            next: (respose) => {
              this.employeeDetails = respose;
            },
          });
        }
      },
    });
  }

  updateEmployee() {
    this.employeeserviceService
      .updateEmployee(this.employeeDetails.id, this.employeeDetails)
      .subscribe({
        next: (response) => {
          this.router.navigate(['employees']);
        },
      });
  }

  deletEmployee(id: string) {
    this.employeeserviceService.deletEmployee(id).subscribe({
      next: (response) => {
        this.router.navigate(['employees']);
      },
    });
  }
}
