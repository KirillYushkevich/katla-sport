import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DepartmentService } from '../services/department.service';
import { DepartmentItem } from '../models/department-item'

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.css']
})
export class DepartmentFormComponent implements OnInit {

  file: File = null;
  department = new DepartmentItem(0, "", "", 0,);
  existed = false;
  departmentslist: DepartmentItem[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private DepartmentService: DepartmentService

  ) { }

  ngOnInit() {
    this.DepartmentService.getDepartments().subscribe(c => this.departmentslist = c);
    this.route.params.subscribe(p => {
      if (p['id'] === undefined) return;
      this.DepartmentService.getDepartment(p['id']).subscribe(h => this.department = h);
      this.existed = true;
    });
  }

  onFileChange(event) {
      this.file = event.target.files[0];
      console.log(this.file);
  }

  navigateTodepartments() {
    this.router.navigate(['/departments']);
  }

  onCancel() {
    this.navigateTodepartments();
  }
  
  onSubmit() {
    if (this.existed) {
      this.DepartmentService.updateDepartment(this.department).subscribe(c => this.navigateTodepartments());
    } else {
      this.DepartmentService.addDepartment(this.department).subscribe(c => this.navigateTodepartments());
    }
  }
}
