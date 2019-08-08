import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import {DoctorItem } from '../models/doctor-item';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private url = environment.apiUrl + 'api/doctor/';

  constructor(private http: HttpClient) { }

  getDoctors(): Observable<Array<DoctorItem>> {
    return this.http.get<Array<DoctorItem>>(this.url);
  }

  getDoctor(doctorid: number): Observable<DoctorItem> {
    return this.http.get<DoctorItem>(`${this.url}${doctorid}`);
  }

  addDoctor(doctor: DoctorItem, file: File): Observable<DoctorItem> {
      console.log(file);
    const formdata = new FormData();
    formdata.append('Image', file);
    formdata.append('data',JSON.stringify(doctor));
    return this.http.post<DoctorItem>(`${this.url}`, formdata);
  }

  updateDoctor(doctor: DoctorItem, file: File): Observable<Object> {
    const formdata = new FormData();
    console.log(file);
    formdata.append('Image', file);
    formdata.append('data',JSON.stringify(doctor));
    return this.http.put<Object>(`${this.url}${doctor.id}`, formdata);
  }

  deleteDoctor(doctorid: number): Observable<Object> {
    return this.http.delete<Object>(`${this.url}${doctorid}`);
  }
}
