import { Component, OnInit } from '@angular/core';
import { DoctorItem } from '../models/doctor-item';
import { DoctorService } from '../services/doctor.service';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css']
})
export class DoctorListComponent implements OnInit {

  doctors: DoctorItem[];

  constructor(private DoctorService: DoctorService) { }

  ngOnInit() {
    this.getDoctors();
  }

  getDoctors() {
    this.DoctorService.getDoctors().subscribe(h => this.doctors = h);
  }

  onDelete(doctorid: number) {
    this.DoctorService.deleteDoctor(doctorid).subscribe(p => this.getDoctors());   
  }
}
