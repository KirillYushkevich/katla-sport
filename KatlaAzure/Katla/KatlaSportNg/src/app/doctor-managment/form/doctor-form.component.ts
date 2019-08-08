import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorService } from '../services/doctor.service';
import { DoctorItem } from '../models/doctor-item'

@Component({
  selector: 'app-Doctor-form',
  templateUrl: './doctor-form.component.html',
  styleUrls: ['./doctor-form.component.css']
})
export class DoctorFormComponent implements OnInit {

  file: File = null;
  Doctor = new DoctorItem(0, "", "", 0, null,);
  existed = false;
  doctorslist: DoctorItem[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private DoctorService: DoctorService

  ) { }

  ngOnInit() {
    this.DoctorService.getDoctors().subscribe(c => this.doctorslist = c);
    this.route.params.subscribe(p => {
      if (p['id'] === undefined) return;
      this.DoctorService.getDoctor(p['id']).subscribe(h => this.Doctor = h);
      this.existed = true;
    });
  }

  onFileChange(event) {
      this.file = event.target.files[0];
      console.log(this.file);
  }

  navigateToDoctors() {
    this.router.navigate(['/Doctors']);
  }

  onCancel() {
    this.navigateToDoctors();
  }
  
  onSubmit() {
    if (this.existed) {
      this.DoctorService.updateDoctor(this.Doctor, this.file).subscribe(c => this.navigateToDoctors());
    } else {
      this.DoctorService.addDoctor(this.Doctor, this.file).subscribe(c => this.navigateToDoctors());
    }
  }
}
