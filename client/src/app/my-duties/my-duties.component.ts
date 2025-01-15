import { Component, inject, OnInit } from '@angular/core';
import { DutiesService } from '../_services/duties.service';
import { Duty } from '../_models/duty';

@Component({
  selector: 'app-my-duties',
  standalone: true,
  imports: [],
  templateUrl: './my-duties.component.html',
  styleUrl: './my-duties.component.scss'
})
export class MyDutiesComponent implements OnInit{
  private dutiesService = inject(DutiesService);
  duties: Duty[] | undefined;
  

  ngOnInit(): void {
      this.getDuties();
  }

  getDuties() {
    this.dutiesService.getDuties().subscribe({
      next: duties => this.duties = duties
    })
  }
}
