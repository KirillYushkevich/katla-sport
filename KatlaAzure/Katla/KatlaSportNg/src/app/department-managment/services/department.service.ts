import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import {DepartmentItem } from '../models/department-item';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private url = environment.apiUrl + 'api/department/';

  constructor(private http: HttpClient) { }

  getDepartments(): Observable<Array<DepartmentItem>> {
    return this.http.get<Array<DepartmentItem>>(this.url);
  }

  getDepartment(departmentid: number): Observable<DepartmentItem> {
    return this.http.get<DepartmentItem>(`${this.url}${departmentid}`);
  }

  addDepartment(department: DepartmentItem): Observable<DepartmentItem> {
    return this.http.post<DepartmentItem>(`${this.url}`, department);
  }

  updateDepartment(department: DepartmentItem): Observable<Object> {
    return this.http.put<Object>(`${this.url}${department.id}`, department);
  }

  deleteDepartment(departmentid: number): Observable<Object> {
    return this.http.delete<Object>(`${this.url}${departmentid}`);
  }
}
