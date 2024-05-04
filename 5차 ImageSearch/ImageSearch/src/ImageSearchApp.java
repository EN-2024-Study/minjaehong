// revalidate repaint 써보기
// 쓰레드처럼 돌아가고 있는 Swing의 EventListener를 호출해줘서 rendering하게 강제한다??
// 쨌든 뭐 변한거 안뜨면 얘 호출하면 된다고 함
// 그냥 add 했는데 안뜨는 이유가 add 는 EventListener 에게 영향을 주지 않기 때문임

// MVC 패턴이 아님 - Delegate 패턴??
// 객체에 동적으로 기능을 추가 또는 변경할 수 있는 패턴
// Swing 은 자체적으로 Delegate 패턴이라고 함
// 얘가 View 랑 Controller 작업을 모두 수행함
// 그래서 얘랑 Model 만 필요함
// 여기서 바로 Model.DAO 호출하고 바로 DTO 로 받기

// 자동정렬 ctrl alt shift L
class ImageSearchApp{

    public static void main(String[] args){

        MainFrame mainFrame = new MainFrame();
    }
}