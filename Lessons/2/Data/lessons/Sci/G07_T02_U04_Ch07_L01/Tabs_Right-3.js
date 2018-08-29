//All general attributes can be commented or deleted
//•	We have seven types of tabs.
//1.	Text
//2.	textWithImage
//3.	textWithVideo
//4.	TextWithSlider
//5.	Slider
//6.	Video
//7.	Image
//•	Any dynamicData attribute must be written in code
//•	Template can run OGG or MP3 audio
//•	Template can run OGV or MP4 video
//•	Write audio or video source without extensions.
var mydata={
"staticData":{
    "pageHeader":"الغيوم",//Template title appears on Header area
    "prompt":"اضغط على كل عنوان لتعرض الشرح الخاص به.",//Blue prompt which appears at page bottom
    "title":'الغيوم',    
    "inside_title":'<img style="display: table;margin: 0 auto;width: 450px;" src="../Data/lessons/Sci/G07_T02_U04_Ch07_L01/images/G07_T02_U04_Ch07_L01_P10.png">',//Text can be written under header above tabs
    "narration":'',//Audio starts on page load
},
"dynamicData":[
//    {
//        "type":"text",//must be written
//        "title":"الدرس الدرس الدرس الد",//Tab title
//        "title_icon":"pics/4.jpg",
//        'sub_narration':'sounds/sub_narration',//•	Narration will start on click on tab – option
//        "paragraph":'الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس  الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس'},
//    
//    {
//        "type":"image",
//        "title":"الاول",
//        "title_icon":"pics/4.jpg",
//        'sub_narration':'sounds/sub_narration',
//        "mediaSource":'pics/4.jpg'},    
//    
//    {
//        "type":"video",
//        "title":"الدرس الدرس بب",
//        "title_icon":"",
//        'sub_narration':'',
//        "mediaSource":"../Player/lessons/ssssss/video/222"},
//    
//    {
//        "type":"slider",
//        "title":"الاول",
//        "title_icon":"pics/4.jpg",
//        'sub_narration':'',
//        "mediaSource":["pics/4.jpg","pics/man.png"]},
    
    {
        "type":"text",
        "title":"الغيوم المنخفضة الغيوم المنخفضة",
//        "title_icon":"pics/4.jpg",
        'sub_narration':'../Data/lessons/Sci/G07_T02_U04_Ch07_L01/audio/G07_T02_U04_Ch07_L01_P10_Tab1',
        "paragraph":'تتكوّن على ارتفاع 2000 م أو أقل من سطح الأرض. ومن أمثلتها الغيوم الركامية؛ وهي غيوم سميكة تتشكّل عندما ترتفع تيارات هوائية رطبة إلى أعلى. وتدلّ الغيوم الركامية أحيانًا على طقس معتدل. ولكن عندما يزداد سُمكها تُنتج أمطارًا غزيرة يصاحبها برق ورعد.',
        "mediaSource":""
    },//Type image source
    
      {
        "type":"text",
        "title":"الغيوم المتوسطة  ",
//        "title_icon":"pics/4.jpg",
        'sub_narration':'../Data/lessons/Sci/G07_T02_U04_Ch07_L01/audio/G07_T02_U04_Ch07_L01_P10_Tab1',
        "paragraph":"تكون على ارتفاعات تتراوح بين 2000 م - 8000 م، وتتكون من خليط من ماء سائل وبلورات جليدية، وقد تسبب أمطارًا خفيفة. ومن أمثلتها: الغيوم الركامية المتوسطة، والغيوم الطبقية المتوسطة.",
          
        "mediaSource":""
      },//Type image source
    
     {
        "type":"text",
        "title":"الغيوم المرتفعة",
//        "title_icon":"pics/4.jpg",
        'sub_narration':'../Data/lessons/Sci/G07_T02_U04_Ch07_L01/audio/G07_T02_U04_Ch07_L01_P10_Tab1',
        "paragraph":"تتكوَّن من بلورات جليدية بسبب وجودها على ارتفاعات كبيرة. ومن أمثلتها: الغيوم الريشية، والغيوم الريشية الركامية، والغيوم الريشية الطبقية. ومن الغيوم نوع آخر يمتدّ عموديًّا على جميع الارتفاعات، ويسمى غيوم المزن الركامية، وتسبب أمطارًا غزيرة وزخّات من الثلج، وقد تولّد عواصف رعدية.",
          
         "mediaSource":""
      },//Type image source
    
    
    
     
    
    
//      {
//        "type":"textWithImage",
//        "title":"الخطوة3",
////        "title_icon":"pics/4.jpg",
////        'sub_narration':'',
//        "paragraph":'ضع أصفار بدلًا من الأرقام الواقعة عن يمين الرقم 2 ، فتكون القيمة المنزلية للرقم 2 هي 2000 ، وذلك لأنه يقع في منزلة آحاد الألوف.',
//        "mediaSource":"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAeoAAADECAYAAABQvGLIAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6NzhCNkZCMDMyNEI3MTFFNjhFNUZGQzM3QzYzRDE4OUQiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6NzhCNkZCMDQyNEI3MTFFNjhFNUZGQzM3QzYzRDE4OUQiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDo3OEI2RkIwMTI0QjcxMUU2OEU1RkZDMzdDNjNEMTg5RCIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDo3OEI2RkIwMjI0QjcxMUU2OEU1RkZDMzdDNjNEMTg5RCIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PiJa5iwAAB/1SURBVHja7J0LkBXV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",
//      },//Type image source
    
//    {
//        "type":"textWithVideo",
//        "title":"الدرس الد الدرس الدرس",
//        "title_icon":"pics/4.jpg",
//        'sub_narration':'',
//        "paragraph":'الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس',
//        "mediaSource":"../Player/lessons/ssssss/video/222"},//Type video source without extension
//    
//    {
//        "type":"TextWithSlider",
//        "title":"الدرس الدرس الدرس",
//        "title_icon":"",
//        'sub_narration':'',
//        "paragraph":'الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس الدرس',
//        "mediaSource":["pics/5.jpg","pics/6.jpg"]}//Type images source in array and each image separated by ,
    ]   
};