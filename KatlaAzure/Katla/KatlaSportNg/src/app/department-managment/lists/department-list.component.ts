import { Component, OnInit } from '@angular/core';
import { DepartmentItem } from '../models/department-item';
import { DepartmentService } from '../services/department.service';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})
export class DepartmentListComponent implements OnInit {

  departments: DepartmentItem[];

  constructor(private DepartmentService: DepartmentService) { }

  ngOnInit() {
    this.getDepartments();
  }

  getDepartments() {
    this.DepartmentService.getDepartments().subscribe(h => this.departments = h);
  }

  onDelete(departmentid: number) {
    this.DepartmentService.deleteDepartment(departmentid).subscribe(p => this.getDepartments());   
  }
}
