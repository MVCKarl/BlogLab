export class Photo {
  constructor(
    public photoId: number,
    public applicationUserId: number,
    public imageUrl: string,
    publicId: string,
    public description: string,
    public publishDate: Date,
    public updateDate: Date,
    public deleteConfirm: boolean = false){
  }
}
