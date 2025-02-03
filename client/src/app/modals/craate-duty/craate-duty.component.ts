import { NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { DutiesService } from '../../_services/duties.service';
import { Duty } from '../../_models/duty';


@Component({
  selector: 'app-craate-duty',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './craate-duty.component.html',
  styleUrl: './craate-duty.component.scss'
})
export class CraateDutyComponent {
  bsModalRef = inject(BsModalRef);
  dutiesServise = inject(DutiesService);
  createdDuty: Duty | null = null;

  duty = {
    name: "",
    frequency: 7,
    roomId: null
  }

  create() {
    this.dutiesServise.addDuty(this.duty).subscribe({
      next: (duty) => {
        this.createdDuty = duty;
        this.bsModalRef.hide();
      }
    })
  }


}
