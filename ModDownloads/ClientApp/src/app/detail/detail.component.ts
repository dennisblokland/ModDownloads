import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DownloadService } from '../download.service'
import { ModService } from '../mod.service';
@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  public name: string;
  private sub: any;
  public DownloadData: Array<any>;
  public IncreaseData: Array<any>;

  public totalDownloads: number;
  public dailyDownloads: number;
  public monthlyDownloads: number;
  public yearlyDownloads: number;

  constructor(private downloadService: DownloadService, private modService: ModService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.name = params.name;
    });

    this.modService.getByName(this.name).subscribe((data: Array<any>) => {
      let DownloadData2 = [];
        this.downloadService.getById(data).subscribe((data: Array<any>) => {
          let set = { name: this.name, series: [] };
          for (let entry of data) {
            set.series.push({
              name: new Date(entry.timestamp),
              value: entry.downloads
            });
          }

          DownloadData2.push(set);
          this.DownloadData = [...DownloadData2];

        });
   
      this.downloadService.getIncreaseById(data).subscribe((data: { [index: string]: any; }) => {
        let IncreaseData2 = [{ name: this.name, series: [] }];
        for (let [key, value] of Object.entries(data)) {
          IncreaseData2[0].series.push({
            name: new Date(key),
            value: value
          });
          this.IncreaseData = [...IncreaseData2];
        }

        

      });
      this.downloadService.getDailyById(data).subscribe((data: number) => {
        this.dailyDownloads = data;
      });
      this.downloadService.getMonthlyById(data).subscribe((data: number) => {
        this.monthlyDownloads = data;
      });
      this.downloadService.getYearlyById(data).subscribe((data: number) => {
        this.yearlyDownloads = data;
      });
      this.downloadService.getTotalById(data).subscribe((data: number) => {
        this.totalDownloads = data;
      });
    });

   
 }
      
  ngOnDestroy() {
    this.sub.unsubscribe();
  }
  view: any[] = [1200, 800];

  // options for the chart
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = false;
  showXAxisLabel = true;
  xAxisLabel = 'Time';
  showYAxisLabel = true;
  yAxisLabel = 'Downloads';
  timeline = true;

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  // line, area
  autoScale = true;
}
