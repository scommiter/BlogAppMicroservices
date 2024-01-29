import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';
import { Subject, takeUntil } from 'rxjs';
import { PageResultDto } from '../../../../shared-lib/src/lib/models/page-result.dto';
import { PostDto } from '../entites/post.dto';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrl: './post.component.scss'
})
export class PostComponent implements OnInit{
  private ngUnsubscribe = new Subject<void>();

  constructor(private postService: PostService) {

  }

  ngOnInit(): void {
    // this.getAllPost();
  }

  getAllPost(){
    this.postService.getProducts(10, 1)
    .pipe(takeUntil(this.ngUnsubscribe))
          .subscribe({
            next: (response: PageResultDto<PostDto>) => {
              console.log("GET ALL POST", response.items);
            },
            error: () => {},
          })
  }
}
