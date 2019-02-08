import { Component } from '@angular/core';
import { DownloadService } from '../download.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public DownloadData: Array<any> = [{ data: [], label: "downloads" }];
  public DownloadLabels: Array<any>;

  constructor(private downloadService: DownloadService) {
    downloadService.get().subscribe((data: Array<any>) => {
      this.DownloadData[0].data = data.map(a => a.downloads);
      this.DownloadLabels = data.map(a => a.timestamp);
      console.log(this.DownloadData);
    });
  }
  public lineChartOptions: any = {
    responsive: true,
    scales: {
      xAxes: [{
        type: 'time',
        time: {
          unit: 'day'
        }
      }],

    }
  }; 

  public lineChartLegend: boolean = true;
  public lineChartType: string = 'line';


  // events
  public chartClicked(e: any): void {
    console.log(e);
  }

  public chartHovered(e: any): void {
    console.log(e);
  }
}
