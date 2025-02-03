import { Component, inject, OnInit, signal } from '@angular/core';
import { DutiesService } from '../_services/duties.service';
import { Duty } from '../_models/duty';
import {NgClass, NgIf} from "@angular/common";
import { ProgressbarComponent, ProgressbarModule, ProgressbarType } from 'ngx-bootstrap/progressbar';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { CraateDutyComponent } from '../modals/craate-duty/craate-duty.component';
import { EditDutyComponent } from '../modals/edit-duty/edit-duty.component';


@Component({
  selector: 'app-my-duties',
  standalone: true,
  imports: [ProgressbarModule, NgClass, NgIf],
  templateUrl: './my-duties.component.html',
  styleUrl: './my-duties.component.scss'
})
export class MyDutiesComponent implements OnInit{
  private dutiesService = inject(DutiesService);
  private bsModalService = inject(BsModalService);
  //bsModalRef?: BsModalRef;
  createBsModalRef: BsModalRef<CraateDutyComponent> = new BsModalRef<CraateDutyComponent>();
  editBsModalRef: BsModalRef<EditDutyComponent> = new BsModalRef<EditDutyComponent>();
  duties = signal<Duty[] | undefined>(undefined);
  
  ngOnInit(): void {
      this.getDuties();
  }

  getDuties() {
    this.dutiesService.getDuties().subscribe({
      next: duties => {
        this.duties.set(duties);
        duties.forEach(duty => {
          duty.daysGone = (this.getTodayDateUTC().getTime() - duty.lastTimeDone.getTime()) / (24 * 60 * 60 * 1000);
          duty.zone = this.defineZone(duty.frequency, duty.daysGone);
          duty.daysLeft = this.calculatePositiveDaysLeft(duty.frequency, duty.daysGone);
        });
        this.sortDutiesByDescending();
      }
    })
  }

  deleteDuty(dutyId: number) {
    this.dutiesService.deleteDuty(dutyId).subscribe({
      next: () => {
        this.duties.set(this.duties()!.filter(d => d.id !== dutyId));
      }
    })
  }

  markAsDone(dutyId: number) {
    this.dutiesService.markDutyAsDone(dutyId).subscribe({
      next: () => {
        this.duties.set(this.duties()!.map(d => {
          if (d.id === dutyId) {
            d.lastTimeDone = this.getTodayDateUTC();
            d.daysGone = 0;
            d.zone = this.defineZone(d.frequency, d.daysGone);
            d.daysLeft = this.calculatePositiveDaysLeft(d.frequency, d.daysGone);
          }
          return d;
        }))
      }
    })
  }

  openAddDutyModal() {
    this.createBsModalRef = this.bsModalService.show(CraateDutyComponent);
    this.createBsModalRef.onHide?.subscribe({
      next: () => {
        if (this.createBsModalRef.content && this.createBsModalRef.content.createdDuty !== null) {
          var newDuty = this.createBsModalRef.content.createdDuty;
          newDuty.daysGone = 0;
          newDuty.zone = this.defineZone(newDuty.frequency, newDuty.daysGone);
          newDuty.daysLeft = newDuty.frequency;

          this.duties.set([...this.duties()!, newDuty])
          this.sortDutiesByDescending();
        }
      }
    });
  }

  openEditDutyModal(duty: Duty) {
    const initialState: ModalOptions = {
      initialState: {
        duty: duty
      }
    }
    this.editBsModalRef = this.bsModalService.show(EditDutyComponent, initialState);
    // this.editBsModalRef.onHide?.subscribe({
    //   next: () => {
    //     if (this.createBsModalRef.content && this.createBsModalRef.content.createdDuty !== null) {
    //       var newDuty = this.createBsModalRef.content.createdDuty;
    //       newDuty.daysGone = 0;
    //       newDuty.zone = this.defineZone(newDuty.frequency, newDuty.daysGone);
    //       newDuty.daysLeft = newDuty.frequency;

    //       this.duties.set([...this.duties()!, newDuty])
    //     }
    //   }
    // });
  }

  getTodayDateUTC(): Date {
    const today = new Date();
    const utcDate = new Date(Date.UTC(today.getUTCFullYear(), today.getUTCMonth(), today.getUTCDate()));
    //const utcDate = new Date(Date.UTC(today.getUTCFullYear(), today.getUTCMonth(), today.getUTCDate()+5));
    return utcDate;
  }

  defineZone(frequency: number, daysGone: number): ProgressbarType {
    if (daysGone > frequency*0.66)
      return "danger";
    if (daysGone > frequency*0.33)
      return "warning";
    return "success";
  }

  calculatePositiveDaysLeft(frequency: number, daysGone: number): number {
    var daysLeft = frequency-daysGone;
    if (daysLeft < 0) 
      daysLeft = 0;

    return daysLeft;
  }

  sortDutiesByDescending() {
    this.duties.set(this.duties()?.sort((d1, d2) => {
      return (d1.frequency-d1.daysGone) - (d2.frequency-d2.daysGone)
    }))
  }
}
