using System;
using UnityEngine;

namespace Assets.Scripts.Markers
{
    public class PickupableItemMarker: MonoBehaviour
    {
        //likvidator: пока так, пока не придумаем обертку над конфигурацией
        //likvidator: хотя я думаю, что мы по такому пути и пойдем и не будет что-то свое писать
        public string Id { get; } = Guid.NewGuid().ToString();

        public string Name;
    }
}