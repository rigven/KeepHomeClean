import { Component, inject, OnInit } from '@angular/core';
import { DutiesService } from '../../_services/duties.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Duty } from '../../_models/duty';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-edit-duty',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './edit-duty.component.html',
  styleUrl: './edit-duty.component.scss'
})
export class EditDutyComponent implements OnInit{
  bsModalRef = inject(BsModalRef);
  dutiesServise = inject(DutiesService);
  duty: Duty | undefined;

  dutyModel: DutyModel = {
    name: "",
    frequency: 0,
    roomId: null
  };

  ngOnInit(): void {
    this.dutyModel.name = this.duty!.name;
    this.dutyModel.frequency = this.duty!.frequency;
    this.dutyModel.roomId = this.duty!.roomId;
  }

  save() {
    this.dutiesServise.editDuty(this.duty!.id, this.dutyModel).subscribe({
      next: () => {
        this.duty!.name = this.dutyModel.name;
        this.duty!.frequency = this.dutyModel.frequency;
        this.duty!.roomId = this.dutyModel.roomId;

        this.bsModalRef.hide();
      }
    })
  }

}

interface DutyModel {
  name: string;
  frequency: number;
  roomId: number | null;
}
