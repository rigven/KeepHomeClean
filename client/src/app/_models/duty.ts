import { ProgressbarType } from "ngx-bootstrap/progressbar";

export interface Duty {
    id: number;
    name: string;
    frequency: number;
    lastTimeDone: Date;
    roomId: number | null;

    //Calculated
    daysGone: number;
    daysLeft: number;
    zone: ProgressbarType;
}